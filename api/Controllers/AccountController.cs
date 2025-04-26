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
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailSender _emailSender;
        public AccountController(
            UserManager<AppUser> userManager, 
            ITokenService tokenService, 
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender
        ){
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email) {
            
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            _emailSender.SendEmailAsync(user.Email, "HEARTH - Reset Password", token);

            return Ok("Password reset Email sent");
        }

        [HttpPost("forgot-password/reset")]
        public async Task<IActionResult> ForgotPassword(string email, string token, string newPassword) {
            
            var user = await _userManager.FindByEmailAsync(email);

            _userManager.ResetPasswordAsync(user, token, newPassword);

            return Ok("Password reset");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    Email = registerDto.Email,
                    UserName = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    EmailConfirmed = true
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    
                    await _userManager.SetTwoFactorEnabledAsync(appUser, true);
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok("Account created. You may now Log in.");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(501, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(502, e);
            }
        }

    

    
    }
}