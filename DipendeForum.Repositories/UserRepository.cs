using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
