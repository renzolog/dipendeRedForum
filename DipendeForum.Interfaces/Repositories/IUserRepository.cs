using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IUserRepository
    {
        UserDomain GetById(Guid id);

        UserDomain GetByEmail(string email);

        UserDomain GetByUsername(string username);

        UserDomain GetByIdLight(Guid id);

        UserDomain GetByEmailLight(string email);

        UserDomain GetByUsernameLight(string username);

        IEnumerable<UserDomain> GetAll();

        void Add(UserDomain user);
    }
}
