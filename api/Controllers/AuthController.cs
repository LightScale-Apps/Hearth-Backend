using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Data;
using api.Dtos.PatientData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Org.BouncyCastle.Asn1.Esf;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto.Engines;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Azure.Core;
using System.Runtime.Intrinsics.Arm;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailSender _emailSender;

        public AuthController( 
            ApplicationDBContext context,
            UserManager<AppUser> userManager, 
            ITokenService tokenService, 
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender
            )
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
             if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid login");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid login");

            var generator = new Random();

            
            user.OTC = generator.Next(0, 9).ToString() + generator.Next(0, 9).ToString() + generator.Next(0, 9).ToString() + generator.Next(0, 9).ToString();
            var message = "<h3>Your HEARTH One-Time Code is below.</h3><br><h1>" + user.OTC + "</h1><br><h6>If you didn't request this code, consider changing your password</h6>";


            await _emailSender.SendEmailAsync(user.Email, "Hearth - One-Time Code", message);

            await _context.SaveChangesAsync();

            return Ok("Check Email");
            
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    

        [HttpPost("{email}/{code}")]
        public async Task<IActionResult> GetRefreshToken([FromRoute] string email, [FromRoute] string code) {
            if (code == "NO_CODE") return Unauthorized("No code");

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.OTC == code && x.Email == email);

            if (user == null) return Unauthorized("Invalid code");
            user.OTC = "NO_CODE";

            var refreshToken = new RefreshToken {
                Token = RandomString(128),
                OwnedBy = user.Id,
                CreatedOn = DateTime.Now
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new TokenDto {
                RefreshToken = refreshToken.Token,
                AccessToken = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> GetAccessToken([FromBody] string refreshToken) {
            //find the user it belongs to
            var tokenSearchResult = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
            if (tokenSearchResult == null) return Unauthorized("Invalid token");

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == tokenSearchResult.OwnedBy);
            if (user == null) return Unauthorized("Invalid token");

            var accessToken = _tokenService.CreateToken(user);

            tokenSearchResult.Token = RandomString(128); //maybe we don't need this
            tokenSearchResult.CreatedOn = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new TokenDto {
                AccessToken = accessToken,
                RefreshToken = tokenSearchResult.Token
            });
        }
    }

}