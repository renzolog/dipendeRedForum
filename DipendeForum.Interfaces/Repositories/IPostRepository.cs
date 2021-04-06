
﻿using System;
 using System.Collections.Generic;
 using DipendeForum.Domain;

namespace DipendeForum.Interfaces.Repositories
{
    public interface IPostRepository 
    {
        void Add(PostDomain post);

        PostDomain GetById(Guid id);

        ICollection<PostDomain> GetAll();

        void Update(PostDomain message);

        void Delete(PostDomain message);
    }
}
