using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notes.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Infrastructure.Security
{
    public static class AuthOptions
    {
        public static Task<string> CreateTokenAsync(User? user, IConfiguration configuration)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (user == null) throw new Exception();

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Name, ClaimTypes.Role);

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Authorization:SecretKey").Value));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var token = new JwtSecurityToken(
                        audience: configuration.GetSection("Authorization:Audience").Value,
                        issuer: configuration.GetSection("Authorization:Issuer").Value,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(double.Parse(configuration.GetSection("Authorization:Lifetime").Value)),
                        signingCredentials: creds);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                catch (Exception)
                {
                    return "";
                }

            });
        }

        public static bool CreatePasswordHash(string password, out byte[]? passwordHash, out byte[]? passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                return true;
            }
            catch (Exception)
            {
                passwordSalt = default;
                passwordHash = default;
                return false;
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
