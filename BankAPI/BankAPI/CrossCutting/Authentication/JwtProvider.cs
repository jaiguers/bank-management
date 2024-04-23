using BankAPI.CrossCutting.AppModelDtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankAPI.CrossCutting.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public string Generate(AuthDTO userAuth)
        {
            var claims = GetClaims(userAuth);
            var sigCredentials = GetSigningCredentials();

            var SecToken = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                sigCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(SecToken);
            return token;
        }


        private static Claim[] GetClaims(AuthDTO userAuth)
        {
            return new Claim[] {
                new(JwtRegisteredClaimNames.Sub, userAuth.Id),
                new (JwtRegisteredClaimNames.Email, userAuth.UserName),
                new (JwtRegisteredClaimNames.Name, userAuth.FullName)
            };
        }

        private SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256
                );
        }
    }
}
