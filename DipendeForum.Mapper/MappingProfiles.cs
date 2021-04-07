using AutoMapper;
using DipendeForum.Context.Entities;
using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MessageDomain, Message>()
                .ReverseMap();

            CreateMap<PostDomain, Post>()
                .ReverseMap();

            CreateMap<UserDomain, User>()
                .ReverseMap();
        }

    }
}
