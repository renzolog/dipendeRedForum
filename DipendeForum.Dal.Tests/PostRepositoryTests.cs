using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xunit;
using System.Linq;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Context.Entities;
using DipendeForum.Context;
using AutoMapper;
using DipendeForum.Repositories;

namespace DipendeForum.Dal.Tests
{
    public class PostRepositoryTests
    {
        private readonly ForumDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly IPostRepository _repo;

        public PostRepositoryTests(ForumDbContext ctx, IMapper mapper, IPostRepository repo)
        {
            _ctx = ctx;
            _mapper = mapper;
            _repo = repo;
        }

        
        
    }
}
