﻿using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Context.Entities;
using DipendeForum.Domain;
using DipendeForum.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DipendeForum.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ForumDbContext _ctx;
        private readonly IMapper _mapper;

        public MessageRepository(ForumDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public void Add(MessageDomain message)
        {

            if (message is null)
            {
                throw new Exception("Cannot add null message.");
            }

            if (_ctx.Message.FirstOrDefault(m => m.Id.Equals(message.Id)) != null ) 
            {
                throw new Exception("A Message with this Id already exists.");
            }

            var messageEntity = _mapper.Map<Message>(message);
            _ctx.Message.Add(messageEntity);
            _ctx.SaveChanges();
        }

        public void Delete(MessageDomain message)
        {
            var messageEntity = _mapper.Map<Message>(message);
            _ctx.Message.Remove(messageEntity);
            _ctx.SaveChanges();
        }

        public ICollection<MessageDomain> GetAll()
        {
            var messageEntities = _ctx.Message;
            var messageDomainList = _mapper.ProjectTo<MessageDomain>(messageEntities).ToList();
            return messageDomainList;
        }

        public MessageDomain GetById(Guid id)
        {
            var message = _ctx.Message.FirstOrDefault(m => m.Id == id);
            var messageEntity = _mapper.Map<MessageDomain>(message);
            return messageEntity;
        }

        public void Update(MessageDomain message)
        {
            var messageEntity = _mapper.Map<Message>(message);
            _ctx.Message.Update(messageEntity);

            _ctx.SaveChanges();
        }

        public void RejectChanges()
        {
            foreach (var entry in _ctx.ChangeTracker.Entries())
            {

                entry.State = EntityState.Detached;
            }
        }
    }
}
