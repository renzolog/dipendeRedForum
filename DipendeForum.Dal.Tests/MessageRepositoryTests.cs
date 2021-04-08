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
using Microsoft.EntityFrameworkCore;

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
            using var transaction = new TransactionScope();
            
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

        [Fact]
        public void GetAll_NoInput_ReturnsListOfMessages()
        {
            var messages = _repo.GetAll();

            Assert.NotEmpty(messages);
        }

        [Fact]
        public void GetById_InputIsValid_ReturnMessage()
        {
            var message = _repo.GetById(Guid.Parse("e6e7a1b7-4153-4dd9-af7e-32822d620950"));

            Assert.NotNull(message);
        }

        [Fact]
        public void GetById_NoMessageFound_Throws()
        {
            var nullMessage = _repo.GetById(Guid.NewGuid());

            Assert.Null(nullMessage);
        }

        [Fact]
        public void Delete_InputIsValid()
        {
            using var transaction = new TransactionScope();

            MessageDomain message = new MessageDomain()
            {
                Id = Guid.NewGuid(),
                Content = "prova",
                PublicationTimestamp = DateTime.Now,
                Post = null,
                User = null
            };
            _repo.Add(message);
            _repo.RejectChanges();
            var messageToDelete = _repo.GetAll().FirstOrDefault(m => m.Id == message.Id);
            _repo.Delete(messageToDelete);
            
            Assert.Throws<Exception>(() => _repo.GetById(message.Id));
        }

        [Fact]
        public void Delete_NoUserFound_Throws()
        {
            using var transaction = new TransactionScope();

            var message = _repo.GetById(Guid.NewGuid());

            _repo.Delete(message);

            Assert.Throws<Exception>(() => _repo.Delete(message));
        }

        [Fact]
        public void Update_InputIsValid()
        {
            using var transaction = new TransactionScope();

            var guid = Guid.NewGuid();

            MessageDomain message = new MessageDomain()
            {
                Id = guid,
                Content = "prova",
                PublicationTimestamp = DateTime.Now,
                Post = null,
                User = null
            };

            _repo.Add(message);

            _repo.RejectChanges();

            message.Content = "provaaaa";

            _repo.Update(message);

            _repo.RejectChanges();

            var message1 = _repo.GetById(guid);

            Assert.Equal(message.Content, message1.Content);
        }
    }
}
