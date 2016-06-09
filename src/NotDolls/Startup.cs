using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotDolls.Models;
using Microsoft.EntityFrameworkCore;

namespace NotDolls
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var connection = @"Server=(localdb)\mssqllocaldb;Database=NotDollsDB;Trusted_Connection=True;";
      services.AddDbContext<NotDollsContext>(options => options.UseSqlServer(connection));

      // Allow any header or method from local dev machine
      services.AddCors(options =>
      {
        options.AddPolicy("AllowDevEnvironment",
            builder => builder.WithOrigins("http://localhost:5000")
                              .AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());
      });

      // Add framework services.
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseMvc();
    }
  }
}
