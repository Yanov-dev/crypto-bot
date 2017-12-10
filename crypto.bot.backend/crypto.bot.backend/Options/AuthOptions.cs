using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace crypto.bot.backend.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Issuer) &&
                               !string.IsNullOrEmpty(Audience) &&
                               !string.IsNullOrEmpty(Key) &&
                               Lifetime > 0;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}