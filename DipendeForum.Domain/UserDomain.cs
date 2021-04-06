using System.Collections.Generic;

namespace DipendeForum.Domain
{
    public class UserDomain
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }

        public List<PostDomain> Posts { get; set; }
        public List<MessageDomain> Messages { get; set; }
    }
}
