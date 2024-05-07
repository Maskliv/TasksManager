using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Interfaces.BL;
using TasksManager.Domain.Interfaces.Validations;
using TasksManager.Domain.Variables;

namespace TasksManager.Application.BL
{
    public class AuthBL : IAuthBL
    {
        private readonly IUserBL _userBL;
        private readonly IUserValidation _userValidation;
        private readonly IConfiguration _configuration;

        public AuthBL(IUserBL userBL, IUserValidation userValidation, IConfiguration configuration)
        {

            _userBL = userBL;
            _userValidation = userValidation;
            _configuration = configuration;
        }

        

        public async Task<string?> Login(LoginDto loginData)
        {
            //Validacion del usuario
            User? user = await _userBL.GetByUsernameAsync(loginData.username);
            if (user == null) return null;
            if (!_userValidation.IsPasswordCorrect(user.Id, loginData.password)) 
                return null;

            var token = GenerateToken(user.Id, user.Username, user.Role);
            return token;

        }

        private string? GenerateToken(int idUser, string username, string role)
        {
            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_configuration[AppSettingsKeys.JWT_SECRET] ?? throw new Exception("JWT Secret not setted yet.")));

            var tokenData = new TokenModel
            {
                Id = idUser,
                Username = username,
                Role = role,
            };

            JwtSecurityToken? token = BuildJwtSecurityToken(authSigningKey, tokenData);

            string? result = null;

            if (token != null)
                result = new JwtSecurityTokenHandler().WriteToken(token);
            
            return result;
        }

        private JwtSecurityToken? BuildJwtSecurityToken(SymmetricSecurityKey authSigningKey, TokenModel tokenData)
        {
            Claim[] claims = new[]
            {
                new Claim("Id", tokenData.Id.ToString()),
                new Claim("Username", tokenData.Username),
                new Claim("Role", tokenData.Role)
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;

        }
    }

    internal class TokenModel
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
}
