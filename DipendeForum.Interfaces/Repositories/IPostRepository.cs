using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Interfaces.Services
{
    public interface IPostRepository
    {
        public void Add();
        public UserDomain GetById();
        public ICollection<PostDomain> GetAll();
        public void Delete();
    }
}
