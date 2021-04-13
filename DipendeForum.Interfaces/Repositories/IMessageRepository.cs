using DipendeForum.Domain;
using System;
using System.Collections.Generic;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        void Add(MessageDomain message);

        MessageDomain GetById(Guid id);

        IEnumerable<MessageDomain> GetAll();

        void Update(MessageDomain message);

        void Delete(MessageDomain message);

        void RejectChanges();
    }
}
