using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DayFlow.Test.DayFlow.Modules.Identity.Test.Infrastructure.Security
{
    public class Argon2PasswordHasherTest
    {
        private Argon2PasswordHasher CreateHasher()
        {
            var options = Options.Create(new Argon2PasswordHasherOptions
            {
                Iterations = 3,
                MemorySize = 64 * 1024,
                DegreeOfParallelism = 2
            });

            return new Argon2PasswordHasher(options);
        }

        [Fact]
        public void Hash_And_Verify_Should_Succeed()
        {
            var hasher = CreateHasher();

            var password = "MySecurePassword123!";

            var hash = hasher.Hash(password);
            var result = hasher.Verify(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void Verify_With_WrongPassword_Should_Fail()
        {
            var hasher = CreateHasher();

            var hash = hasher.Hash("correct-password");

            var result = hasher.Verify("wrong-password", hash);

            Assert.False(result);
        }

        [Fact]
        public void AddSecurity_Should_Register_IPasswordHasher()
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Security:Argon2PasswordHasher:Iterations"] = "3",
                    ["Security:Argon2PasswordHasher:MemorySize"] = "65536",
                    ["Security:Argon2PasswordHasher:DegreeOfParallelism"] = "2"
                })
                .Build();

            services.AddSecurity(config);

            var provider = services.BuildServiceProvider();

            var hasher = provider.GetService<IPasswordHasher>();

            Assert.NotNull(hasher);
            Assert.IsType<Argon2PasswordHasher>(hasher);
        }
    }
}
