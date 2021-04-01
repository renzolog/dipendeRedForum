using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DipendeForum.Domain
{
    public class MessageDomain
    {
        public Guid Id { get; set; }
        public PostDomain Post { get; set; }
        public UserDomain User { get; set; }
        public string Content { get; set; }
    }
}
