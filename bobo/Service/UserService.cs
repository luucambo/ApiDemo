using System;
using System.Collections.Generic;
using bobo.Interfaces;
using System.Linq;
using bobo.Utilities;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace bobo.Service
{
    public class UserService : IUserService
    {
        public ServiceResponse<string> Authenticate(UserCred userCred)
        {
            var response = new ServiceResponse<string>();
            //kiểm tra user có tồn tại trong hệ thống hay không
            if (!DummyDatabase.UserCreds.Any(p => p.Username == userCred.Username && p.Password == userCred.Password))
            {
                response.Success = false;
                response.AddMessage("User not found");
                return response;
            }

            //tạo token
            var tokenKey = Encoding.ASCII.GetBytes(AppConstants.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, userCred.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.Data = tokenHandler.WriteToken(token);
            response.Success = !string.IsNullOrEmpty(response.Data);
            return response;
        }

        public ServiceResponse Register(UserCred userCred)
        {
            var response = new ServiceResponse<string>();
            if (!string.IsNullOrEmpty(userCred.Username) && !string.IsNullOrEmpty(userCred.Password))
            {
                if (DummyDatabase.UserCreds.Any(p => p.Username == userCred.Username && p.Password == userCred.Password))
                {
                    response.AddMessage("user existed");
                    response.Success = false;
                    return response;
                }
                DummyDatabase.UserCreds.Add(userCred);
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.AddMessage("user info is not valid");
            }

            return response;
        }
    }
}