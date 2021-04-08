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

        [Fact]
        public void GetById_InputIsValid_Returns_PostDomain()
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

        [Fact]
        public void GetById_InputIsEmpty_Throws()
        {
            Assert.Throws<Exception>(() => _repo.GetById(Guid.Empty));
        }       

        [Fact]
        public void GetById_PostNotFound_Throws()
        {
            Assert.Throws<Exception>(() => _repo.GetById(Guid.NewGuid()));
        }
        
        [Fact] 
        public void Add_InputIsValid()
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

        [Fact]
        public void Add_InputIsNull_Throws()
        {
            using (var transaction = new TransactionScope())
            {

                Assert.Throws<Exception>(() => _repo.Add(null));

            }
        }

        [Fact]
        public void Add_IdAlreadyExists_Throws()
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

                Assert.Throws<Exception>(() => _repo.Add(_mapper.Map<PostDomain>(postino)));
            }
        }
    }
}
