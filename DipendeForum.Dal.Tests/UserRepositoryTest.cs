using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Context.Entities;
using DipendeForum.Domain;
using DipendeForum.Mapper;
using DipendeForum.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DipendeForum.Dal.Tests
{
    public class UserRepositoryTest
    {
        private readonly IMapper _mapper;

        public UserRepositoryTest()
        {
            var config = new MapperConfiguration(opt => opt.AddProfile<MappingProfiles>());
            _mapper = new AutoMapper.Mapper(config);
        }

        [Fact]
        public void GetById_InputGuidIdIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIdIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = repository.GetById(tempUser.Id);

                    Assert.NotNull(toTest);
                    Assert.IsType<UserDomain>(toTest);
                    Assert.Equal(tempUser.Id, toTest.Id);
                    Assert.Equal(tempUser.Username, toTest.Username);
                    Assert.Equal(tempUser.Password, toTest.Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetById_InputGuidIdIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIdIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var toTest = repository.GetById(Guid.NewGuid());

                Assert.Null(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmail_InputStringEmailIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmail_InputStringEmailIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = repository.GetByEmail(tempUser.Email);

                    Assert.NotNull(toTest);
                    Assert.IsType<UserDomain>(toTest);
                    Assert.Equal(tempUser.Id, toTest.Id);
                    Assert.Equal(tempUser.Username, toTest.Username);
                    Assert.Equal(tempUser.Password, toTest.Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmail_InputStringEmailIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmail_InputStringEmailIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var toTest = repository.GetByEmail(Guid.NewGuid().ToString());

                Assert.Null(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsername_InputStringUsernameIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsername_InputStringUsernameIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = repository.GetByUsername(tempUser.Username);

                    Assert.NotNull(toTest);
                    Assert.IsType<UserDomain>(toTest);
                    Assert.Equal(tempUser.Id, toTest.Id);
                    Assert.Equal(tempUser.Username, toTest.Username);
                    Assert.Equal(tempUser.Password, toTest.Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsername_InputStringUsernameIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsername_InputStringUsernameIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var toTest = repository.GetByUsername(Guid.NewGuid().ToString());

                Assert.Null(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByIdLight_InputGuidIdIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByIdLight_InputGuidIdIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = repository.GetByIdLight(tempUser.Id);

                    Assert.NotNull(toTest);
                    Assert.IsType<UserDomain>(toTest);
                    Assert.Equal(tempUser.Id, toTest.Id);
                    Assert.Equal(tempUser.Username, toTest.Username);
                    Assert.Equal(tempUser.Password, toTest.Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByIdLight_InputGuidIdIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByIdLight_InputGuidIdIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var toTest = repository.GetByIdLight(Guid.NewGuid());

                Assert.Null(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmailLight_InputStringEmailIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmailLight_InputStringEmailIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = repository.GetByEmailLight(tempUser.Email);

                    Assert.NotNull(toTest);
                    Assert.IsType<UserDomain>(toTest);
                    Assert.Equal(tempUser.Id, toTest.Id);
                    Assert.Equal(tempUser.Username, toTest.Username);
                    Assert.Equal(tempUser.Password, toTest.Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmailLight_InputStringEmailIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmailLight_InputStringEmailIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var toTest = repository.GetByEmailLight(Guid.NewGuid().ToString());

                Assert.Null(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsernameLight_InputStringUsernameIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsernameLight_InputStringUsernameIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = repository.GetByUsernameLight(tempUser.Username);

                    Assert.NotNull(toTest);
                    Assert.IsType<UserDomain>(toTest);
                    Assert.Equal(tempUser.Id, toTest.Id);
                    Assert.Equal(tempUser.Username, toTest.Username);
                    Assert.Equal(tempUser.Password, toTest.Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsernameLight_InputStringUsernameIsNotValid_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsernameLight_InputStringUsernameIsNotValid_ReturnsNull")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var toTest = repository.GetByUsernameLight(Guid.NewGuid().ToString());

                Assert.Null(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetAll_NoInput_ReturnsIEnumerableOfUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetAll_NoInput_ReturnsIEnumerableOfUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var firstTempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    var secondTempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test2",
                        Email = "test2",
                        Password = "test2",
                        ProfilePicture = "test2",
                        Role = "1"
                    };

                    context.User.Add(firstTempUser);
                    context.User.Add(secondTempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(2, entries);

                    var toTest = repository.GetAll().ToList();

                    Assert.Equal(2, toTest.Count());
                    Assert.IsType<List<UserDomain>>(toTest);
                    Assert.Equal(firstTempUser.Id, toTest[0].Id);
                    Assert.Equal(firstTempUser.Username, toTest[0].Username);
                    Assert.Equal(firstTempUser.Password, toTest[0].Password);
                    Assert.Equal(firstTempUser.ProfilePicture, toTest[0].ProfilePicture);
                    Assert.Equal(firstTempUser.Role, toTest[0].Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetAll_NoInput_ReturnsEmptyIEnumerable()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetAll_NoInput_ReturnsEmptyIEnumerable")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                var toTest = repository.GetAll();

                Assert.Empty(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Add_InputUserDomainObject_NoReturn()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputUserDomainObject_NoReturn")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);

                using (var transaction = new TransactionScope())
                {
                    var tempUserDomain = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    repository.Add(tempUserDomain);

                    var toTest = context.User.FirstOrDefault(u => u.Id.Equals(tempUserDomain.Id));

                    Assert.NotNull(toTest);
                    Assert.IsType<User>(toTest);
                    Assert.Equal(tempUserDomain.Id, toTest.Id);
                    Assert.Equal(tempUserDomain.Username, toTest.Username);
                    Assert.Equal(tempUserDomain.Password, toTest.Password);
                    Assert.Equal(tempUserDomain.ProfilePicture, toTest.ProfilePicture);
                    Assert.Equal(tempUserDomain.Role, toTest.Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

    }
}