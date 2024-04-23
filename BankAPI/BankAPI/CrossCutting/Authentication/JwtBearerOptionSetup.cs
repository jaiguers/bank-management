using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankAPI.CrossCutting.Authentication
{
    public class JwtBearerOptionSetup : IConfigureOptions<JwtBearerOptions>
    {
        private JwtOptions jwtOptions;

        public JwtBearerOptionSetup(IOptions<JwtOptions> jwtOpt)
        {
            jwtOptions = jwtOpt.Value;
        }

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };
        }
    }
}
