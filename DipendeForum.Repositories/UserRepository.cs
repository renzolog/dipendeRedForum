using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DipendeForum.Context.Entities;
using DipendeForum.Domain;
using Microsoft.EntityFrameworkCore;

namespace DipendeForum.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _ctx;
        private readonly IMapper _mapper;

        public UserRepository(ForumDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public UserDomain GetById(Guid id)
        {
            var user = _ctx.User
                .Include(u => u.Posts)
                .Include(u => u.Messages)
                .FirstOrDefault(u => u.Id.Equals(id));
            return user == null ? null : _mapper.Map<UserDomain>(user);
        }

        public UserDomain GetByEmail(string email)
        {
            var user = _ctx.User
                .Include(u => u.Posts)
                .Include(u => u.Messages)
                .FirstOrDefault(u => u.Email.Equals(email));
            return user == null ? null : _mapper.Map<UserDomain>(user);
        }

        public UserDomain GetByUsername(string username)
        {
            var user = _ctx.User
                .Include(u => u.Posts)
                .Include(u => u.Messages)
                .FirstOrDefault(u => u.Username.Equals(username));
            return user == null ? null : _mapper.Map<UserDomain>(user);
        }

        public UserDomain GetByIdLight(Guid id)
        {
            var user = _ctx.User
                .FirstOrDefault(u => u.Id.Equals(id));
            return user == null ? null : _mapper.Map<UserDomain>(user);
        }

        public UserDomain GetByEmailLight(string email)
        {
            var user = _ctx.User
                .FirstOrDefault(u => u.Email.Equals(email));
            return user == null ? null : _mapper.Map<UserDomain>(user);
        }

        public UserDomain GetByUsernameLight(string username)
        {
            var user = _ctx.User
                .FirstOrDefault(u => u.Username.Equals(username));
            return user == null ? null : _mapper.Map<UserDomain>(user);
        }

        public IEnumerable<UserDomain> GetAll()
        {
            var users = _ctx.User;
            return _mapper.ProjectTo<UserDomain>(users).ToList();
        }

        public void Add(UserDomain user)
        {
            var userDomain = _mapper.Map<User>(user);
            _ctx.User.Add(userDomain);
            _ctx.SaveChanges();
        }
    }
}
