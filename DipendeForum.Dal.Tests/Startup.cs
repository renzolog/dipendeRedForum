using DipendeForum.Context;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Mapper;
using DipendeForum.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;


namespace DipendeForum.Dal.Tests
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ForumDbContext>(
                opt => opt.UseSqlServer("Server=DESKTOP-0K9C6PL;Database=ForumDb;User id=sa;Password=root"));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
