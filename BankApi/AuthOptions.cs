using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankApi
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "jsdfjkksdjklj;sdkl;sdkl;jsdklsdklsdkl;klds!123";   // ключ для шифрации
        public const int LIFETIME = 120; // время жизни токена - 120 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
