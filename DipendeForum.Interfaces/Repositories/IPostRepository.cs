using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Interfaces.Services
{
    interface IPostRepository
    {
        public void Add();
        public User GetById();
        public ICollection<Post> GetAll();
        public void Delete();
    }
}
