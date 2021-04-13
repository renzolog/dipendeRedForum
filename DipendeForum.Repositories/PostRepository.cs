using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DipendeForum.Context;
using DipendeForum.Domain;
using DipendeForum.Interfaces.Repositories;
using System.Linq;
using DipendeForum.Context.Entities;

namespace DipendeForum.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumDbContext _ctx;
        private readonly IMapper _mapper;

        public PostRepository(ForumDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public void Add(PostDomain post)
        {
            var toAdd = _mapper.Map<Post>(post);
            _ctx.Post.Add(toAdd);
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var toDelete = _ctx.Post.FirstOrDefault(p => p.Id == id);
            _ctx.Remove(toDelete);
            _ctx.SaveChanges();
        }

        public PostDomain GetById(Guid id)
        {

            Post postEntity = _ctx.Post
                .FirstOrDefault(p => p.Id == id);

            PostDomain postDomain = _mapper.Map<PostDomain>(postEntity);

            return postDomain;
        }
     
        public IEnumerable<PostDomain> GetAll()
        {
            var postDomains = _mapper.ProjectTo<PostDomain>(_ctx.Post).ToList();
            return postDomains;
        }

        public void Update(PostDomain post)
        {
            throw new NotImplementedException();
        }
    }
}
