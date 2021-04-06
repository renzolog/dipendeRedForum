﻿using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Domain;
using DipendeForum.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
            //var messageEntity = _mapper.Map<Message>(message);
            //_ctx.Message.Add(messageEntity);
            //_ctx.SaveChanges();
        }

        public void Delete(MessageDomain element)
        {
            throw new NotImplementedException();
        }

        public ICollection<MessageDomain> GetAll()
        {
            //var messageEntities = _ctx.Message;
            //var messageDomainList = _mapper.ProjectTo<MessageDomain>(messageEntities).ToList();
            //return messageDomainList;
            throw new NotImplementedException();
        }

        public MessageDomain GetById(Guid id)
        {
            //var message = _ctx.Message.FirstOrDefault(m => m.Id == id);
            //var messageEntity = _mapper.Map<Message>(message);
            //return messageEntity;
            throw new NotImplementedException();
        }

        public void Update(MessageDomain element)
        {
            throw new NotImplementedException();
        }
    }
}
