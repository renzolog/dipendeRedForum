using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Interfaces.Services
{
    public interface IUserRepository
    {
        public void Add();
        public UserDomain GetById();
        public ICollection<UserDomain> GetAll();
        public void Delete();
    }
}
