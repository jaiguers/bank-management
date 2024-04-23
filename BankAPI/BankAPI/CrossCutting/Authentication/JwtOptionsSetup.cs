using Microsoft.Extensions.Options;

namespace BankAPI.CrossCutting.Authentication
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration config;
        private const string SECTION_NAME = "Jwt";

        public JwtOptionsSetup(IConfiguration configuration)
        {
            config = configuration;
        }

        public void Configure(JwtOptions options)
        {
            config.GetSection(SECTION_NAME).Bind(options);
        }
    }
}
