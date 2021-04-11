using System;
using DipendeForum.Domain;

namespace DipendeForum.Mvc.Services
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CustomAuthorize : Attribute
    {
        public Role Role { get; }

        public CustomAuthorize(Role role)
        {
            Role = role;
        }
    }
}