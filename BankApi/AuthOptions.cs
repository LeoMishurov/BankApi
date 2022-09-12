using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankApi
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // Издатель токена
        public const string AUDIENCE = "MyAuthClient"; // Потребитель токена
        const string KEY = "jsdfjkksdjklj;sdkl;sdkl;jsdklsdklsdkl;klds!123"; // Ключ для шифрации
        public const int LIFETIME = 120; // Время жизни токена - 120 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
