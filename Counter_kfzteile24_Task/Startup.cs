using System;
using System.Net;
using Counter.Command;
using Counter.Repository;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Counter.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            //read from app settins file based on envirinment i.e development, test, ..etc
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Core2.1
            services.AddMvc();
            #endregion
            #region Event Sourcing
            //build Event Store Connection
            var connection = BuildEventStoreConnection();
            services.AddSingleton(connection);
            #endregion
            #region DI
            //each project responsible for his own DI
            services.AddCommand();
            services.AddRepository();
            #endregion
            #region Swagger
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Counter API", Version = "v1" });
            });
            #endregion
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEventStoreConnection connection)
        {
            #region Dev
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                #region Swagger
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger(c => { });
                // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Counter API V1");
                });

                #endregion
            }

            #endregion

            #region Core2.1
            app.UseMvc();
            app.UseStatusCodePages();
            #endregion

            #region Event sourcing
            // wait to complete the connection
            connection.ConnectAsync().Wait();
            #endregion


        }
        private IEventStoreConnection BuildEventStoreConnection()
        {
            //default port
            const int defaultport = 1113;
            //get user name and password based on environment
            string username = Configuration.GetSection("AppConfiguration:username").Value;
            string password = Configuration.GetSection("AppConfiguration:password").Value;
            var creds = new UserCredentials(username, password);
            var settings = ConnectionSettings.Create()
                .SetDefaultUserCredentials(creds)
                .SetHeartbeatInterval(TimeSpan.FromSeconds(10))
                .Build();
            //create connection
            var connection = EventStore.ClientAPI.EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, defaultport));
            return connection;
        }

    }
}
