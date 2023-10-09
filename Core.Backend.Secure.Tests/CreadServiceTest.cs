using Core.Backend.Secure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Backend.Secure.Tests
{
    public class CredServiceTest :
        IClassFixture<WebAppFactory<Program>>
    {
        private readonly WebAppFactory<Program> factory;

        public CredServiceTest(WebAppFactory<Program> factory)
        {
            this.factory = factory;
        }


        [Theory]
        [InlineData("ölkj")]
        public void Decrypt_Succeed(string password)
        {
            var credService = this.factory.Services.GetRequiredService<CredService>();
            credService.DecryptPw("password");
        }
    }
}
