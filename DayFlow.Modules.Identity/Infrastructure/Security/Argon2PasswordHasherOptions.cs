namespace DayFlow.Modules.Identity.Infrastructure.Security
{
    public class Argon2PasswordHasherOptions
    {
        public string Algorithm { get; set; } = "Argon2id";

        public int DegreeOfParallelism { get; set; } = 2;

        public int Iterations { get; set; } = 3;

        public int MemorySize { get; set; } = 64 * 1024;
    }
}
