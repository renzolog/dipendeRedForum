using DipendeForum.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Repositories
{
    class UserRepository : IUserRepository
    {
        ICollection<User> IUserRepository.GetAll()
        {
            ICollection<User> allUsers = User
        }

        User IUserRepository.GetById()
        {

        }

        void IUserRepository.Add()
        {
            
        }

        void IUserRepository.Delete()
        {
            
        }

    }
}
