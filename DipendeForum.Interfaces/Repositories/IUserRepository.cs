using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Interfaces.Services
{
    public interface IUserRepository
    {
        public void Add();
        public User GetById();
        public ICollection<User> GetAll();
        public void Delete();
    }
}
