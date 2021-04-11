using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Context.Entities;
using DipendeForum.Domain;
using DipendeForum.Interfaces.CustomExceptions;
using DipendeForum.Mapper;
using DipendeForum.Repositories;
using DipendeForum.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace DipendeForum.Presentation.Tests
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            var config = new MapperConfiguration(opt => opt.AddProfile<MappingProfiles>());
            _mapper = new AutoMapper.Mapper(config);
        }

        [Fact]
        public void GetById_InputGuidIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetById(tempUser.Id);

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
        public void GetById_InputGuidIsNotFound_ThrowsSearchedNotFoundException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetById_InputGuidIsNotFound_ThrowsSearchedNotFoundException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<SearchedNotFoundException>(() => service.GetById(Guid.NewGuid()));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmail_InputStringIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmail_InputStringIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetByEmail(tempUser.Email);

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
        public void GetByEmail_InputStringIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmail_InputStringIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.GetByEmail(string.Empty));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmail_InputStringIsNotFound_ThrowsSearchedNotFoundException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmail_InputStringIsNotFound_ThrowsSearchedNotFoundException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<SearchedNotFoundException>(() => service.GetByEmail("prova@gmail.com"));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmail_InputStringFormatIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmail_InputStringFormatIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.GetByEmail("prova"));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsername_InputStringIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsername_InputStringIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetByUsername(tempUser.Username);

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
        public void GetByUsername_InputStringIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsername_InputStringIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.GetByUsername(string.Empty));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsername_InputStringIsNotFound_ThrowsSearchedNotFoundException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsername_InputStringIsNotFound_ThrowsSearchedNotFoundException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<SearchedNotFoundException>(() => service.GetByUsername("prova"));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByIdLight_InputGuidIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByIdLight_InputGuidIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetByIdLight(tempUser.Id);

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
        public void GetByIdLight_InputGuidIsNotFound_ThrowsSearchedNotFoundException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByIdLight_InputGuidIsNotFound_ThrowsSearchedNotFoundException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<SearchedNotFoundException>(() => service.GetByIdLight(Guid.NewGuid()));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmailLight_InputStringIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmailLight_InputStringIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetByEmailLight(tempUser.Email);

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
        public void GetByEmailLight_InputStringIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmailLight_InputStringIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.GetByEmailLight(string.Empty));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmailLight_InputStringIsNotFound_ThrowsSearchedNotFoundException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmailLight_InputStringIsNotFound_ThrowsSearchedNotFoundException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<SearchedNotFoundException>(() => service.GetByEmailLight("prova@gmail.com"));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByEmailLight_InputStringFormatIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByEmailLight_InputStringFormatIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.GetByEmailLight("prova"));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsernameLight_InputStringIsValid_ReturnsUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsernameLight_InputStringIsValid_ReturnsUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetByUsernameLight(tempUser.Username);

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
        public void GetByUsernameLight_InputStringIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsernameLight_InputStringIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.GetByUsernameLight(string.Empty));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetByUsernameLight_InputStringIsNotFound_ThrowsSearchedNotFoundException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetByUsernameLight_InputStringIsNotFound_ThrowsSearchedNotFoundException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<SearchedNotFoundException>(() => service.GetByUsernameLight("prova"));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetAll_NoInput_ReturnsListOfUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetAll_NoInput_ReturnsListOfUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "test1",
                        Password = "test1Test@",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.GetAll();

                    Assert.NotEmpty(toTest);
                    Assert.Single(toTest);
                    Assert.IsType<List<UserDomain>>(toTest);
                    Assert.Equal(tempUser.Id, toTest[0].Id);
                    Assert.Equal(tempUser.Username, toTest[0].Username);
                    Assert.Equal(tempUser.Password, toTest[0].Password);
                    Assert.Equal(tempUser.ProfilePicture, toTest[0].ProfilePicture);
                    Assert.Equal(tempUser.Role, toTest[0].Role);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetAll_NoInput_ReturnsEmptyListOfUserDomainObject()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetAll_NoInput_ReturnsEmptyListOfUserDomainObject")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.GetAll();

                Assert.Empty(toTest);
                Assert.IsType<List<UserDomain>>(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Add_InputUserDomainObjectIsValid_NoReturn()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputUserDomainObjectIsValid_NoReturn")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    service.Add(tempUser);

                    var toTest = context.User.FirstOrDefault(u => u.Id.Equals(tempUser.Id));

                    Assert.NotNull(toTest);
                    Assert.IsType<User>(toTest);
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
        public void Add_InputUserDomainObjectEmailNotValid_ThrowsAlreadyExistsException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputUserDomainObjectEmailNotValid_ThrowsAlreadyExistsException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var alreadyExistsUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test125",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    service.Add(alreadyExistsUser);

                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test123",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    Assert.Throws<AlreadyExistsException>(() => service.Add(tempUser));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Add_InputUserDomainObjectUsernameNotValid_ThrowsAlreadyExistsException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputUserDomainObjectUsernameNotValid_ThrowsAlreadyExistsException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var alreadyExistsUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    service.Add(alreadyExistsUser);

                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova1@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    Assert.Throws<AlreadyExistsException>(() => service.Add(tempUser));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Add_InputUserDomainObjectPasswordIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputUserDomainObjectPasswordIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test",
                        Email = "prova@gmail.com",
                        Password = "test",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    Assert.Throws<InvalidInputException>(() => service.Add(tempUser));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Add_InputUserDomainObjectIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("Add_InputUserDomainObjectIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.Add(null));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsUsernameInUse_InputStringIsValid_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsUsernameInUse_InputStringIsValid_ReturnsTrue")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.IsUsernameInUse("test1");
                    Assert.True(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsUsernameInUse_InputStringIsValid_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsUsernameInUse_InputStringIsValid_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.IsUsernameInUse("test123");
                    Assert.False(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsEmailInUse_InputStringIsValid_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsEmailInUse_InputStringIsValid_ReturnsTrue")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.IsEmailInUse("prova@gmail.com");
                    Assert.True(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsEmailInUse_InputStringIsValid_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsEmailInUse_InputStringIsValid_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Equal(1, entries);

                    var toTest = service.IsEmailInUse("prova2@gmail.com");
                    Assert.False(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValid_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValid_ReturnsTrue")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("Test@Test1");
                Assert.True(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValidButShorterThan8_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValidButShorterThan8_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("Test@T1");
                Assert.False(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValidButBiggerThan16_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValidButBiggerThan16_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("Test@Test@Test@Test1");
                Assert.False(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValidButNoUpper_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValidButNoUpper_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("test@te1t");
                Assert.False(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValidButNoLower_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValidButNoLower_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("TEST@T1ST");
                Assert.False(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValidButNoSymbols_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValidButNoSymbols_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("Tests1ests");
                Assert.False(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsValidButNoNumbers_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsValidButNoNumbers_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                var toTest = service.IsPasswordValid("Tests@tests");
                Assert.False(toTest);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void IsPasswordValid_InputStringIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("IsPasswordValid_InputStringIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                Assert.Throws<InvalidInputException>(() => service.IsPasswordValid(string.Empty));

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void DoesPasswordMatchEmail_InputsTwoStringIsValid_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("DoesPasswordMatchEmail_InputsTwoStringIsValid_ReturnsTrue")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    service.Add(tempUser);

                    var toTest = service.DoesPasswordMatchEmail("Test@Test1", tempUser.Email);
                    Assert.True(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void DoesPasswordMatchEmail_InputsTwoStringIsValid_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("DoesPasswordMatchEmail_InputsTwoStringIsValid_ReturnsFalse")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    service.Add(tempUser);

                    var toTest = service.DoesPasswordMatchEmail("Test@Test101", tempUser.Email);
                    Assert.False(toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void DoesPasswordMatchEmail_InputsTwoStringFirstIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("DoesPasswordMatchEmail_InputsTwoStringFirstIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Throws<InvalidInputException>(() =>
                        service.DoesPasswordMatchEmail(string.Empty, "prova@gmail.com"));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void DoesPasswordMatchEmail_InputsTwoStringSecondIsNotValid_ThrowsInvalidInputException()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("DoesPasswordMatchEmail_InputsTwoStringSecondIsNotValid_ThrowsInvalidInputException")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    context.User.Add(tempUser);
                    var entries = context.SaveChanges();

                    Assert.Throws<InvalidInputException>(() =>
                        service.DoesPasswordMatchEmail("Test@Test1", string.Empty));

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetUserRole_InputGuidIsValid_ReturnsEnumTypeRole()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase("GetUserRole_InputGuidIsValid_ReturnsEnumTypeRole")
                .Options;

            using (var context = new ForumDbContext(options))
            {
                var repository = new UserRepository(context, _mapper);
                var encryption = new EncryptionService();
                var service = new UserService(repository, encryption);

                using (var transaction = new TransactionScope())
                {
                    var tempUser = new UserDomain
                    {
                        Id = Guid.NewGuid(),
                        Username = "test1",
                        Email = "prova@gmail.com",
                        Password = "Test@Test1",
                        ProfilePicture = "test1",
                        Role = "1"
                    };

                    service.Add(tempUser);

                    var toTest = service.GetUserRole(tempUser.Id);
                    Assert.IsType<Role>(toTest);
                    Assert.Equal(Role.Admin, toTest);

                    transaction.Complete();
                }

                context.Database.EnsureDeleted();
            }
        }
    }
}