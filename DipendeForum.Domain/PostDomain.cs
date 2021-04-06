using System;
using System.Collections.Generic;

namespace DipendeForum.Domain
{
    public class PostDomain
    {
        public Guid Id { get; set; }
        public UserDomain User { get; set; }
        public string Title { get; set; }
        public List<MessageDomain> Messages { get; set; }
    }
}
