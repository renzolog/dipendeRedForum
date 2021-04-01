using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DipendeForum.Context.Entities
{
    public class User
    {
        [Index(IsUnique = true)]
        public string Username { get; set; }
    }
}
