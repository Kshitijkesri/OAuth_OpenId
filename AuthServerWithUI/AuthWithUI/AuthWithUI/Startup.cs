using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialNetwork.OAuth.Configuration;

namespace AuthWithUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
               //.AddSigningCredential(new X509Certificate2(@"C:\Program Files\OpenSSL-Win64\bin\auth.pfx", ""))
               .AddSigningCredential(new X509Certificate2(@"auth.pfx", ""))
               .AddTestUsers(InMemoryConfiguration.Users().ToList())
               .AddInMemoryClients(InMemoryConfiguration.Clients())
               .AddInMemoryIdentityResources(InMemoryConfiguration.IdentityResources())
               .AddInMemoryApiResources(InMemoryConfiguration.ApiResources());
            services.AddCors(m => m.AddPolicy("localhost", o =>
           o.WithOrigins("http://132.186.195.49:4200", "http://localhost:4200", "https://localhost:4200", "http://132.186.195.70:5000",
           "https://132.186.195.70:5000")
           .AllowAnyMethod() // mising part
           .AllowAnyHeader() // mising part
           ));
            services.AddMvc();
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
         
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
           
            app.UseStaticFiles();
            
            //app.UseCors("AllowMyOrigin");
            app.UseMvcWithDefaultRoute();
        }
    }
}
