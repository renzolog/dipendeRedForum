using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DipendeForum.Context.Entities
{
    public class User
    {
        [Index(IsUnique = true), Required] 
        public string Username { get; set; }
        [Key] 
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }
        public byte[] ProfilePicture { get; set; }
        public ICollection<Post> Posts { get; set; } //?
        public ICollection<Message> Messages { get; set; } //?

    }
}
