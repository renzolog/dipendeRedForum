using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Interfaces.Services
{
    interface IMessageRepository
    {
        public void Add();
        public User GetById();
        public ICollection<Message> GetAll();
        public void Update();
        public void Delete();
    }
}
