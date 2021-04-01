using System;
using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IPostRepository : ICrudRepository<PostDomain, Guid>
    {
        
    }
}
