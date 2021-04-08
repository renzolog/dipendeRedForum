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

            if (post is null)
            {
                throw new Exception("Post to add is invalid.");
            }

            if (_ctx.Post.FirstOrDefault(p => p.Id.Equals(post.Id)) != null)
            {
                throw new Exception("Post ID already exists.");
            } 

            var toAdd = _mapper.Map<Post>(post);
            _ctx.Post.Add(toAdd);
        }

        public void Delete(PostDomain message)
        {
            throw new NotImplementedException();
        }

        public ICollection<PostDomain> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostDomain GetById(Guid id)
        {

            Post postEntity = _ctx.Post
                .FirstOrDefault(p => p.Id == id);

            if (postEntity is null)
            {
                throw new Exception("This post was not found.");
            }

            PostDomain postDomain = _mapper.Map<PostDomain>(postEntity);

            return postDomain;
        }

        public void Update(PostDomain message)
        {
            throw new NotImplementedException();
        }
    }
}
