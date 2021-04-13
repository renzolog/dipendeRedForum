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

        void Update(Guid id);

        void Delete(Guid id);

        void RejectChanges();
    }
}
