using DayFlow.Modules.Identity.Application.Security;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace DayFlow.Modules.Identity.Infrastructure.Security
{
    /// <summary>
    /// Argon2id 密碼雜湊（現代銀行 / SaaS 推薦）
    /// </summary>
    public class Argon2PasswordHasher : IPasswordHasher
    {

        private readonly Argon2PasswordHasherOptions _options;

        public Argon2PasswordHasher(IOptions<Argon2PasswordHasherOptions> options)
        {
            _options = options.Value;
        }

        // 安全參數（可依系統負載調整）
        private const int SaltSize = 16;
        private const int KeySize = 32;

        // memory-hard parameters（越高越安全但越慢）
        private int Iterations => _options.Iterations;
        private int MemorySize => _options.MemorySize;
        private int DegreeOfParallelism => _options.DegreeOfParallelism;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                Iterations = Iterations,
                MemorySize = MemorySize,
                DegreeOfParallelism = DegreeOfParallelism
            };

            byte[] hash = argon2.GetBytes(KeySize);

            return $"{Iterations}.{MemorySize}.{DegreeOfParallelism}." +
                   $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');

            if (parts.Length != 5)
                return false;

            int iterations = int.Parse(parts[0]);
            int memory = int.Parse(parts[1]);
            int parallelism = int.Parse(parts[2]);

            byte[] salt = Convert.FromBase64String(parts[3]);
            byte[] expectedHash = Convert.FromBase64String(parts[4]);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                Iterations = iterations,
                MemorySize = memory,
                DegreeOfParallelism = parallelism
            };

            byte[] actualHash = argon2.GetBytes(expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
