using System;
using System.Collections.Generic;
using System.Text;
using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Services
{
    public interface IMessageService
    {
        void Add(MessageDomain message);

        MessageDomain GetById(Guid id);

        public IEnumerable<MessageDomain> GetAll();

        void Update(Guid id);

        void Delete(Guid id);

        bool DoesMessageExist(Guid id);
    }
}
