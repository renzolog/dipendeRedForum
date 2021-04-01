using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Interfaces.Services
{
    public interface IMessageRepository
    {
        public void Add();
        public UserDomain GetById();
        public ICollection<MessageDomain> GetAll();
        public void Update();
        public void Delete();
    }
}
