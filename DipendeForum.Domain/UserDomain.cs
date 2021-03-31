using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Domain
{
    public class UserDomain
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte ProfilePicture { get; set; }

        public ICollection<PostDomain> Posts { get; set; }
        public ICollection<MessageDomain> Messages { get; set; }
    }
}
