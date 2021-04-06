using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IMessageRepository : ICrudRepository<MessageDomain, Guid>
    {
        void Add(MessageDomain message);

        MessageDomain GetById(Guid id);

        ICollection<MessageDomain> GetAll();

        void Update(MessageDomain message);

        void Delete(MessageDomain message);
    }
}
