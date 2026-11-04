using System.Security.Cryptography;
using System.Text;
using Application.Contracts;

namespace Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string GetHashed(string password)
    {
        byte[] inputPassHashBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashPassBytes = SHA256.HashData(inputPassHashBytes);
        return Convert.ToHexString(hashPassBytes);
    }
}