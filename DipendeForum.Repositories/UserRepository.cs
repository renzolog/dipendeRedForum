using DipendeForum.Context;
using DipendeForum.Domain;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ForumDbContext _context;

        public UserRepository(ForumDbContext context)
        {
            _context = context;
        }

        public List<UserDomain> GetAll() 
        {
            var users = _context.User;
            List<UserDomain> userDomainList = users
                .Select(user => user.)
                .ToList();

            return userDomainList;
        }
    }
}
