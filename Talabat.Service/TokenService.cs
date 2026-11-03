using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateToken(AppUser user, UserManager<AppUser> userManager)
        {
            #region PayLoad
            var AuthClaim = new List<Claim>()
            {
            new Claim(ClaimTypes.GivenName,user.DisplayName),
            new Claim(ClaimTypes.Email,user.Email),
            };

            var UserRoles = await userManager.GetRolesAsync(user);
            foreach (var role in UserRoles)
            {
                AuthClaim.Add(new Claim(ClaimTypes.Role, role));
            }
            #endregion

            #region Key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            #endregion

            #region Header [Key+Algorithm]
            var creds = new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256);
            #endregion

            #region Generate And Create Token

            var token = new JwtSecurityToken(
                   issuer: _configuration["JWT:ValidIssuer"],
                   audience: _configuration["JWT:ValidAudince"],
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                    claims: AuthClaim,
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

            #endregion


        }
    }
}
