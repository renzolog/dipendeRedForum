using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IUserRepository
    {
        UserDomain Get(string id);
    }
}
