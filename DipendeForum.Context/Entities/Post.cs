using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DipendeForum.Context.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Title { get; set; }
        public List<Message> Messages { get; set; } 
    }
}
