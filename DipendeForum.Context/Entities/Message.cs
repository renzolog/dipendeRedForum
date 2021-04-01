using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DipendeForum.Context.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
