using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StoreCRM
{
	public class AuthOptions
	{
        public const string ISSUER = "StoreCrmIssuer";
        public const string AUDIENCE = "StoreCrmClient";
        private const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME_HOURS = 24;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
