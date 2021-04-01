using DipendeForum.Domain;
using System;
using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IMessageRepository : ICrudRepository<MessageDomain, Guid>
    {
        
    }
}
