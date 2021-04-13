using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DipendeForum.Domain;
using DipendeForum.Interfaces.CustomExceptions;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Interfaces.Services;

namespace DipendeForum.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public void Add(MessageDomain message)
        {
            if (message == null)
                throw new InvalidInputException("message null");

            if (DoesMessageExist(message.Id))
                throw new AlreadyExistsException("message Id already exist");

            _repository.Add(message);
        }

        public void Delete(Guid id)
        {
            if (id == null)
                throw new InvalidInputException("message null");

            _repository.Delete(id);
        }

        public IEnumerable<MessageDomain> GetAll()
        {
            var messages = _repository.GetAll().ToList();
            return messages;
        }

        public MessageDomain GetById(Guid id)
        {
            var message = _repository.GetById(id);
            if (message == null)
                throw new SearchedNotFoundException("message");

            return message;
        }

        public void Update(Guid id)
        {
            if (id == null)
                throw new InvalidInputException("message null");

            _repository.Update(id);
        }

        public bool DoesMessageExist(Guid id)
        {
            try
            {
                var message = GetById(id);
                return true;
            }
            catch (SearchedNotFoundException)
            {
                return false;
            }
        }
    }
}
