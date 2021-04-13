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
using DipendeForum.Domain;
using DipendeForum.Mapper;
using Microsoft.EntityFrameworkCore;

namespace DipendeForum.Dal.Tests
{
    public class PostRepositoryTests
    {
        private readonly IMapper _mapper;
        private readonly ForumDbContext _ctx;
        private readonly IPostRepository _repo;

        public PostRepositoryTests(IMapper mapper, ForumDbContext context, IPostRepository repo)
        {
            _mapper = mapper;
            _ctx = context;
            _repo = repo;
        }

        [Fact]
        public void GetById_InputPostDomainIsValid_Returns_PostDomain()
        {
            using (var transaction = new TransactionScope())
            {

                User userino = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "userino@test.com",
                    Messages = null,
                    Password = "heheheh",
                    Posts = null,
                    ProfilePicture = "lol",
                    Role = "asd",
                    Username = "userino89"
                };

                _ctx.User.Add(userino);
                _ctx.SaveChanges();

                Post postino = new Post()
                {
                    Id = Guid.NewGuid(),
                    Messages = null,
                    Title = "None of your business",
                    User = userino
                };

                _ctx.Post.Add(postino);
                _ctx.SaveChanges();

                var result = _repo.GetById(postino.Id);

                Assert.NotNull(result);
                Assert.IsType<PostDomain>(result);

            }   
        }

        /*
        [Fact]
        public void GetById_InputGuidIsEmpty_Throws()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIsEmpty_Throws")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new PostRepository(context, _mapper);

                Assert.Throws<Exception>(() => repository.GetById(Guid.Empty));
            }
        }       
        */

        [Fact]
        public void GetById_PostNotFound_ReturnsNull()
        {
            Assert.Null(_repo.GetById(Guid.NewGuid()));

            _ctx.Database.EnsureDeleted();
        }
        
        [Fact] 
        public void Add_InputPostDomainIsValid_No_Returns()
        {
            using (var transaction = new TransactionScope())
            {
                User userino = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "userino@test.com",
                    Messages = null,
                    Password = "heheheh",
                    Posts = null,
                    ProfilePicture = "lol",
                    Role = "asd",
                    Username = "userino89"
                };

                _ctx.User.Add(userino);
                _ctx.SaveChanges();

                Post postino = new Post()
                {
                    Id = Guid.NewGuid(),
                    Messages = null,
                    Title = "None of your business",
                    User = userino
                };

                _ctx.Post.Add(postino);
                _ctx.SaveChanges();

                var result = _ctx.Post
                    .FirstOrDefault(p => p.Id.Equals(postino.Id));

                Assert.NotNull(result);
                Assert.IsType<Post>(result);

            }
        }

        /*
        [Fact]
        public void Add_InputPostDomainIsNull_Throws()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputPostDomainIsNull_Throws")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new PostRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    Assert.Throws<Exception>(() => repository.Add(null));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }    
        }
        */

        /*
        [Fact]
        public void Add_IdAlreadyExists_Throws()
        {

            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_IdAlreadyExists_Throws")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new PostRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    User userino = new User()
                    {
                        Id = Guid.NewGuid(),
                        Email = "userino@test.com",
                        Messages = null,
                        Password = "heheheh",
                        Posts = null,
                        ProfilePicture = "lol",
                        Role = "asd",
                        Username = "userino89"
                    };

                    context.User.Add(userino);
                    context.SaveChanges();

                    Post postino = new Post()
                    {
                        Id = Guid.NewGuid(),
                        Messages = null,
                        Title = "None of your business",
                        User = userino
                    };

                    context.Post.Add(postino);
                    context.SaveChanges();

                    Assert.Throws<Exception>(() => repository.Add(_mapper.Map<PostDomain>(postino)));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }
        */

        [Fact]
        public void Delete_InputGuidIsValid_NoReturns()
        {
            
            using (var transaction = new TransactionScope())
            {

                User userino = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "userino@test.com",
                    Messages = null,
                    Password = "heheheh",
                    Posts = null,
                    ProfilePicture = "lol",
                    Role = "asd",
                    Username = "userino89"
                };

                _ctx.User.Add(userino);
                _ctx.SaveChanges();

                Post postino = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "None of your business",
                    Messages = null,
                    User = userino
                };

                _ctx.Post.Add(postino);
                _ctx.SaveChanges();

                var toDelete = _mapper.Map<PostDomain>(postino);
                _repo.Delete(toDelete.Id);
                _ctx.SaveChanges();

                var toTest = _ctx.Post.FirstOrDefault(p => p.Id == postino.Id);

                Assert.Null(toTest);

            }
        }

        /*
        [Fact]
        public void Delete_InputGuidIsNotValid_Throws()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Delete_InputPostDomainIsValid_NoReturns")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new PostRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {

                    User userino = new User()
                    {
                        Id = Guid.NewGuid(),
                        Email = "userino@test.com",
                        Messages = null,
                        Password = "heheheh",
                        Posts = null,
                        ProfilePicture = "lol",
                        Role = "asd",
                        Username = "userino89"
                    };

                    context.User.Add(userino);
                    context.SaveChanges();

                    Post postino = new Post()
                    {
                        Id = Guid.NewGuid(),
                        Title = "None of your business",
                        Messages = null,
                        User = userino
                    };

                    context.Post.Add(postino);
                    context.SaveChanges();

                    repository.Delete(Guid.NewGuid());
 
                }
            }
        }
        */

        [Fact]
        public void GetAll_NoInput_ReturnsPostDomainIEnumerable()
        {
            
            using (var transaction = new TransactionScope())
            {

                User userino = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "userino@test.com",
                    Messages = null,
                    Password = "heheheh",
                    Posts = null,
                    ProfilePicture = "lol",
                    Role = "asd",
                    Username = "userino89"
                };

                _ctx.User.Add(userino);
                _ctx.SaveChanges();

                Post postino1 = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "None of your business",
                    Messages = null,
                    User = userino
                };

                Post postino2 = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "None of your business I said",
                    Messages = null,
                    User = userino
                };

                _ctx.Post.Add(postino1);
                _ctx.Post.Add(postino2);
                _ctx.SaveChanges();

                var result = _repo.GetAll();

                Assert.IsAssignableFrom<IEnumerable<PostDomain>>(result);
                Assert.NotEmpty(result);

            }
        }

        [Fact]
        public void GetAll_NoInput_ReturnsEmptyIEnumerable()
        {
            using (var transaction = new TransactionScope())
            {
                var toTest =_repo.GetAll();

                Assert.Empty(toTest);
            }
        }
    }
}
