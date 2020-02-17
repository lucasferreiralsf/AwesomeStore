using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AwesomeStore.Domain.DTOs;
using AwesomeStore.Domain.Entities;
using AwesomeStore.Domain.Interfaces.Services.User;
using AwesomeStore.Domain.Repository;
using AwesomeStore.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AwesomeStore.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration;
        public LoginService(IUserRepository repository, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            _repository = repository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }


        public async Task<object> FindByEmail(LoginDto user)
        {
            var baseUser = new UserEntity();
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByEmail(user.Email);
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Authentication failed."
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        }
                    );

                    DateTime createdDate = DateTime.Now;
                    DateTime expirationDate = createdDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createdDate, expirationDate, handler);
                    return SuccessObject(createdDate, expirationDate, token, user);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Authentication failed."
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createdDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfiguration.SigningCredentials,
                    Subject = identity,
                    NotBefore = createdDate,
                    Expires = expirationDate,
                }
            );

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createdDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                createdDate = createdDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                token,
                email = user.Email,
                message = "User is success logedin"
            };
        }
    }
}