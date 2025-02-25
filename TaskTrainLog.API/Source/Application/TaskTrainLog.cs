using TT.Core;

namespace TT.Log;

public class TaskTrainLog : ITTApp
{
    #region Startup initializer

    private class Initialize 
    {
        private IConfiguration Configuration { get; }

        public Initialize(IHostEnvironment env) 
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/appsettings.json", false, true)
                .AddJsonFile($"Config/appsettings.{env.EnvironmentName}.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddJwtAuth();
            services.AddAuthorization();
            services.AddSwaggerGenAuth();
            services.AddControllers();
            services.AddLogListner();
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
        {
            builder.UseRouting();

            builder.UseAuthentication();
            builder.UseAuthorization();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            builder.UseSwagger();
            builder.UseSwaggerUI();
        }
    }

    #endregion

    private IHost _app;

    public void Build(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureWebHostDefaults(webBuilder => 
        {
            webBuilder.UseStartup<Initialize>()
                .UseKestrel()
                .UseUrls("http://*:5003");
        });
        _app = builder.Build();
    }

    public void Run() 
    {
        _app.Run();
    }
}
