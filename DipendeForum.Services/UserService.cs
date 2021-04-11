using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DipendeForum.Domain;
using DipendeForum.Interfaces.CustomExceptions;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Interfaces.Services;

namespace DipendeForum.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncryptionService _service;

        public UserService(IUserRepository repository, IEncryptionService service)
        {
            _repository = repository;
            _service = service;
        }

        public UserDomain GetById(Guid id)
        {
            var user = _repository.GetById(id);
            if (user == null)
                throw new SearchedNotFoundException("user");

            return user;
        }

        public UserDomain GetByEmail(string email)
        {
            if (email.Equals(string.Empty))
                throw new InvalidInputException("email is null");

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                throw new InvalidInputException(email);
            }

            var user = _repository.GetByEmail(email);
            if (user == null)
                throw new SearchedNotFoundException("user");

            return user;
        }

        public UserDomain GetByUsername(string username)
        {
            if (username.Equals(string.Empty))
                throw new InvalidInputException("username null");

            var user = _repository.GetByUsername(username);
            if (user == null)
                throw new SearchedNotFoundException("No user with this username");

            return user;
        }

        public UserDomain GetByIdLight(Guid id)
        {
            var user = _repository.GetByIdLight(id);
            if (user == null)
                throw new SearchedNotFoundException("user");

            return user;
        }

        public UserDomain GetByEmailLight(string email)
        {
            if (email.Equals(string.Empty))
                throw new InvalidInputException("email is null");

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                throw new InvalidInputException(email);
            }

            var user = _repository.GetByEmailLight(email);
            if (user == null)
                throw new SearchedNotFoundException("user");

            return user;
        }

        public UserDomain GetByUsernameLight(string username)
        {
            if (username.Equals(string.Empty))
                throw new InvalidInputException("username null");

            var user = _repository.GetByUsernameLight(username);
            if (user == null)
                throw new SearchedNotFoundException("No user with this username");

            return user;
        }

        public List<UserDomain> GetAll()
        {
            var users = _repository.GetAll().ToList();
            return users;
        }

        public void Add(UserDomain user)
        {
            if (user == null)
                throw new InvalidInputException("user null");

            if (IsUsernameInUse(user.Username))
                throw new AlreadyExistsException(user.Username);

            if (IsEmailInUse(user.Email))
                throw new AlreadyExistsException(user.Email);

            if (!IsPasswordValid(user.Password))
                throw new InvalidInputException("password");

            user.Password = _service.EncryptPassword(user.Password);
            var role = int.Parse(user.Role);
            user.Role = _service.EncryptRole(role);

            _repository.Add(user);
        }

        public bool IsUsernameInUse(string username)
        {
            try
            {
                var user = GetByUsername(username);
                return true;
            }
            catch (SearchedNotFoundException)
            {
                return false;
            }
        }

        public bool IsEmailInUse(string email)
        {
            try
            {
                var user = GetByEmail(email);
                return true;
            }
            catch (SearchedNotFoundException)
            {
                return false;
            }
        }

        public bool IsPasswordValid(string password)
        {
            if (password == string.Empty)
                throw new InvalidInputException("password");

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$";

            return Regex.IsMatch(password, pattern);
        }

        public bool DoesPasswordMatchEmail(string password, string email)
        {
            if (password == string.Empty)
                throw new InvalidInputException("password");

            if (email == string.Empty)
                throw new InvalidInputException("password");

            var user = GetByEmail(email);

            return _service.IsPasswordMatching(user.Password, password);
        }

        public Role GetUserRole(Guid userId)
        {
            var user = GetById(userId);
            var role = _service.DecryptRole(user.Role);

            return role;
        }
    }
}