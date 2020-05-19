using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prototype.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private double _tokenExpiry;

        public AuthenticationService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenExpiry = Convert.ToDouble(_configuration.GetValue<string>("TokenExpireIn"));
        }
        
        public async Task<IdentityResult> SignUp(SignupDTO signupUser)
        {
            var appUser = new ApplicationUser()
            {
                Email = signupUser.Email,
                FirstName = signupUser.FirstName,
                MiddleName = signupUser.MiddleName,
                LastName = signupUser.LastName,
                UserName = signupUser.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, signupUser.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(appUser, "customer");
            }
            return result;
        }

        public async Task<SigninResponseDTO> SignIn(string userName, string password)
        {
            if (!(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)))
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, password))
                    {
                        IList<string> userRoles = await _userManager.GetRolesAsync(user);
                        if (userRoles.Count > 0)
                        {
                            return new SigninResponseDTO()
                            {
                                UserName = user.UserName,
                                FirstName = user.FirstName,
                                Token = GenerateJwtToken(user, userRoles),
                                ExpireIn = _tokenExpiry.ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecuredKey"));
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
            foreach(string role in roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddSeconds(_tokenExpiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}