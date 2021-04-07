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
                opt => opt.UseSqlServer("Data Source=FEDERICO\\SQLEXPRESS; Initial Catalog=ForumDb; User id=sa; Password=root"));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IMessageRepository, MessageRepository>();

        }
    }
}
