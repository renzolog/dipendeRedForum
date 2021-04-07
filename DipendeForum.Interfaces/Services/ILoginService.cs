using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Services
{
    public interface ILoginService
    {
        string EncryptPassword(string password);

        string EncryptEmail(string email);

        string EncryptRole(Role role);

        bool ComparePassword(string id, string password);

        string DecryptEmail(string id);

        int CheckAuthorization(string id);

        bool CompareEmail(string id, string email);
    }
}
