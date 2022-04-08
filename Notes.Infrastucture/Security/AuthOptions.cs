using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastructure.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "Server"; // издатель токена
        public const string AUDIENCE = "Client"; // потребитель токена
        const string KEY = "SecretKey61";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 1 минута


        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
