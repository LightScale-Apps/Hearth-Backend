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
            user.OTC = generator.Next().ToString() + "." + generator.Next().ToString();
            
            await _emailSender.SendEmailAsync(user.Email, "Hearth - One-Time Code", user.OTC);

            await _context.SaveChangesAsync();

            return Ok("Check Email");
            
        }
    

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAccessToken([FromRoute] string code) {
            if (code == "NO_CODE") return Unauthorized("No code");
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.OTC == code);

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