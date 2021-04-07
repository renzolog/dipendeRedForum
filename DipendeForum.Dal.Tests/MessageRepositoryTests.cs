using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Domain;
using DipendeForum.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xunit;
using System.Linq;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Context.Entities;

namespace DipendeForum.Dal.Tests
{
    public class MessageRepositoryTests
    {

        private readonly ForumDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly IMessageRepository _repo;

        public MessageRepositoryTests(ForumDbContext ctx, IMapper mapper, IMessageRepository repo)
        {
            _ctx = ctx;
            _mapper = mapper;
            _repo = repo;
        }

        [Fact]
        public void Add_InputIsValid()
        {
            using (var transaction = new TransactionScope())
            {
                MessageDomain messageDomain = new MessageDomain()
                {
                    Id = Guid.NewGuid(),
                    Content = "prova",
                    PublicationTimestamp = DateTime.Now,
                    Post = null,
                    User = null
                };

                _repo.Add(messageDomain);

                var result = _ctx.Message.FirstOrDefault(r => r.Id == messageDomain.Id);

                Assert.NotNull(result);
            }
  
        }
    }
}
