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
    

        [HttpGet("{email}/{code}")]
        public async Task<IActionResult> GetAccessToken([FromRoute] string email, [FromRoute] string code) {
            if (code == "NO_CODE") return Unauthorized("No code");
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.OTC == code && x.Email == email);

            if (user == null) return Unauthorized("Invalid code");
            user.OTC = "NO_CODE";
            await _context.SaveChangesAsync();;

            return Ok(new NewUserDto {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }
    }

}