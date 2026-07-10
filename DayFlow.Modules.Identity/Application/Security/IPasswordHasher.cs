namespace DayFlow.Modules.Identity.Application.Security
{
    public interface IPasswordHasher
    {
        string Hash(string? password);
        bool Verify(string? password, string? hashedPassword);
    }
}
