using System;

namespace DipendeForum.Domain
{
    public class MessageDomain
    {
        public Guid Id { get; set; }
        public PostDomain Post { get; set; }
        public UserDomain User { get; set; }
        public string Content { get; set; }
        public DateTime PublicationTimestamp { get; set; }
    }
}
