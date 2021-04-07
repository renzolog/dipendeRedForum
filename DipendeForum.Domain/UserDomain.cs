using System.Collections.Generic;

namespace DipendeForum.Domain
{
    public class UserDomain
    {
        public string Username { get; set; }
        public byte[] Email { get; set; }
        public byte[] Password { get; set; }
        public string ProfilePicture { get; set; }
        public byte[] Role { get; set; }

        public List<PostDomain> Posts { get; set; }
        public List<MessageDomain> Messages { get; set; }
    }
}
