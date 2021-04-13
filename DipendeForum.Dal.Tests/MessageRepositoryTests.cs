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
using DipendeForum.Interfaces.CustomExceptions;
using DipendeForum.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace DipendeForum.Dal.Tests
{
    public class MessageRepositoryTests
    {

        private readonly IMapper _mapper;

        public MessageRepositoryTests()
        {
            var config = new MapperConfiguration(opt => opt.AddProfile<MappingProfiles>());
            _mapper = new AutoMapper.Mapper(config);
        }

        [Fact]
        public void Add_InputMessageDomainObject_NoReturn()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                    .UseInMemoryDatabase("Add_InputMessageDomainObject_NoReturn")
                    .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new MessageRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    MessageDomain messageDomain = new MessageDomain
                    {
                        Id = Guid.NewGuid(),
                        Content = "prova",
                        PublicationTimestamp = DateTime.Now,
                        Post = null,
                        User = null
                    };

                    repository.Add(messageDomain);

                    var result = context.Message.FirstOrDefault(m => m.Id == messageDomain.Id);

                    Assert.NotNull(result);
                    Assert.IsType<Message>(result);
                    Assert.Equal(messageDomain.Id, result.Id);
                    Assert.Equal(messageDomain.Content, result.Content);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        //[Fact]
        //public void Add_MessageDomainObjectIsNull_ThrowsInvalidInputException()
        //{
        //    var options = new DbContextOptionsBuilder<ForumDbContext>()
        //        .UseInMemoryDatabase("Add_MessageDomainObjectIsNull_ThrowsInvalidInputException")
        //        .Options;

        //    using (var context = new ForumDbContext(options))
        //    {
        //        var repository = new MessageRepository(context, _mapper);
        //        var result = repository.GetById(Guid.NewGuid());

        //        Assert.Throws<InvalidInputException>(() => repository.Add(result));

        //        context.Database.EnsureDeleted();
        //    }
        //} 

        //[Fact]
        //public void Add_IdAlreadyExists_ThrowsAlreadyExistsException()
        //{
        //    var options = new DbContextOptionsBuilder<ForumDbContext>()
        //        .UseInMemoryDatabase("Add_InputMessageDomainObject_NoReturn")
        //        .Options;

        //    using (var context = new ForumDbContext(options))
        //    {
        //        var repository = new MessageRepository(context, _mapper);

        //        using (var transaction = new TransactionScope())
        //        {
        //            var guid = Guid.NewGuid();

        //            MessageDomain messageDomain = new MessageDomain
        //            {
        //                Id = guid,
        //                Content = "prova",
        //                PublicationTimestamp = DateTime.Now,
        //                Post = null,
        //                User = null
        //            };

        //            repository.Add(messageDomain);

        //            MessageDomain messageDomain2 = new MessageDomain
        //            {
        //                Id = guid,
        //                Content = "prova",
        //                PublicationTimestamp = DateTime.Now,
        //                Post = null,
        //                User = null
        //            };

        //            Assert.Throws<AlreadyExistsException>(() => repository.Add(messageDomain2));

        //            transaction.Complete();
        //        }

        //        context.Database.EnsureDeleted();
        //    }
        //}

        [Fact]
        public void GetAll_NoInput_ReturnsListOfMessages()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetAll_NoInput_ReturnsListOfMessages")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new MessageRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var message = new Message
                    {
                        Id = Guid.NewGuid(),
                        Content = "prova",
                        Post = null,
                        User = null
                    };

                    var message2 = new Message
                    {
                        Id = Guid.NewGuid(),
                        Content = "prova2",
                        Post = null,
                        User = null
                    };

                    context.Message.Add(message);
                    context.Message.Add(message2);
                    var entries = context.SaveChanges();

                    Assert.Equal(2, entries);

                    var result = repository.GetAll().ToList();

                    Assert.Equal(2, result.Count());
                    Assert.IsType<List<MessageDomain>>(result);
                    Assert.NotNull(result);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetById_InputIsValid_ReturnMessageDomain()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputIsValid_ReturnMessageDomain")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new MessageRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var message = new Message
                    {
                        Id = Guid.NewGuid(),
                        Content = "prova",
                        Post = null,
                        User = null
                    };

                    context.Message.Add(message);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var result = repository.GetById(message.Id);

                    Assert.NotNull(result);
                    Assert.IsType<MessageDomain>(result);
                    Assert.Equal(message.Id, result.Id);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetById_InputGuidIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIdIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new MessageRepository(context, _mapper);

                var result = repository.GetById(Guid.NewGuid());

                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Delete_InputIsValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Delete_InputIsValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new MessageRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var message = new Message
                    {
                        Id = Guid.NewGuid(),
                        Content = "prova",
                        Post = null,
                        User = null
                    };

                    context.Message.Add(message);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var messageToDelete = repository.GetById(message.Id);
                    repository.RejectChanges();

                    Assert.NotNull(messageToDelete);

                    repository.Delete(messageToDelete);

                    var toTest = context.Message.FirstOrDefault(m => m.Id == messageToDelete.Id);
                    Assert.Null(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Update_InputIsValid()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIdIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new MessageRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var guid = Guid.NewGuid();

                    MessageDomain message = new MessageDomain()
                    {
                        Id = guid,
                        Content = "prova",
                        PublicationTimestamp = DateTime.Now,
                        Post = null,
                        User = null
                    };

                    repository.Add(message);

                    repository.RejectChanges();

                    message.Content = "provaaaa";

                    repository.Update(message);

                    repository.RejectChanges();

                    var message1 = repository.GetById(guid);

                    Assert.Equal(message.Content, message1.Content);
                }
            }
        }
    }
}
