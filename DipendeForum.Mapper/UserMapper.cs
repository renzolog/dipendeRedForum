using DipendeForum.Context.Entities;
using DipendeForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Mapper
{
    public class UserMapper
    {
        /*
        public static UserDomain UserToUserDomain(User user)
        {
            UserDomain userDomain = new UserDomain();

            userDomain.Email = user.Email;
            userDomain.Username = user.Username;
            userDomain.Password = user.Password;
            
            // userDomain.Posts  
            // userDomain.Messages 
        }
        */

        public static UserDomain UserToUserDomainLite(User user)
        {
            UserDomain userDomain = new UserDomain();

            userDomain.Email = user.Email;
            userDomain.Username = user.Username;
            userDomain.Password = user.Password;
            userDomain.ProfilePicture = user.ProfilePicture;

            userDomain.Posts = null;
            userDomain.Messages = null;

            return userDomain;
        }
    }
}
