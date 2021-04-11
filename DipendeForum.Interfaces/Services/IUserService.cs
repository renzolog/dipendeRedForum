using System;
using System.Collections.Generic;
using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Services
{
    public interface IUserService
    {
        UserDomain GetById(Guid id);
        
        UserDomain GetByEmail(string email);
        
        UserDomain GetByUsername(string username);

        UserDomain GetByIdLight(Guid id);

        UserDomain GetByEmailLight(string email);

        UserDomain GetByUsernameLight(string username);

        List<UserDomain> GetAll();

        void Add(UserDomain user);

        bool IsUsernameInUse(string username);

        bool IsEmailInUse(string email);

        bool IsPasswordValid(string password);

        bool DoesPasswordMatchEmail(string password, string email);

        Role GetUserRole(Guid userId);

    }
}