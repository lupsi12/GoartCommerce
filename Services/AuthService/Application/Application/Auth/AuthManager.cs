using Application.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Context;
using Domain.Entities;

namespace Core.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthManager(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string Login(AuthLoginRequest authLoginRequest)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == authLoginRequest.Email && x.Password == authLoginRequest.Password);
            if (user == null)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name), // Kullanıcı adını kullanmak daha iyi olabilir
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:ValidIssuer"],
                audience: _configuration["Authentication:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}