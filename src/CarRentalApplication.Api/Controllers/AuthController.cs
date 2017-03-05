using CarRentalApplication.Models;
using CarRentalApplication.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CarRentalApplication.Api.Controllers
{
    [Route("api/auth/")]
    public class AuthController : Controller
    {
        private IConfigurationRoot _config;
        private IPasswordHasher<AppUser> _passwordHasher;
        private UserManager<AppUser> _userMgr;

        public AuthController(UserManager<AppUser> userMgr,
            IPasswordHasher<AppUser> passwordHasher,
            IConfigurationRoot config)
        {
            _userMgr = userMgr;
            _passwordHasher = passwordHasher;
            _config = config;
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel lvm)
        {
            try
            {
                var user = await _userMgr.FindByEmailAsync(lvm.Email);
                if(user != null)
                {
                    if(_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, lvm.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {                            
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var credentials = new SigningCredentials(encryptionKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: _config["Tokens:Issuer"],
                            audience: _config["Tokens:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials : credentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception)
            {
            }
            return BadRequest("Failed to generate Token");
        }
    }
}
