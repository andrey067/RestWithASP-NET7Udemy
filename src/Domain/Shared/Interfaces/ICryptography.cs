namespace Domain.Shared.Interfaces
{
    public interface ICryptography
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
