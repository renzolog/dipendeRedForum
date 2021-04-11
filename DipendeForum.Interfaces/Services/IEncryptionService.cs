using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Services
{
    public interface IEncryptionService
    {
        string EncryptPassword(string password);

        string EncryptRole(int role);

        bool IsPasswordMatching(string password, string toCheck);

        Role DecryptRole(string encryptedRole);
    }
}
