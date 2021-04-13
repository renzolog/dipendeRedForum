using AutoMapper.Configuration;
using DipendeForum.Context;
using DipendeForum.Interfaces.Repositories;
using DipendeForum.Mapper;
using DipendeForum.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DipendeForum.Dal.Tests
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddDbContext<ForumDbContext>(opt =>
                opt.UseSqlServer("Data Source = FEDERICO\\SQLEXPRESS; Initial Catalog = ForumDb; User id = sa; Password = root"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        }
    }
}
