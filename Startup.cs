using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_typed_routes
{
  public class Startup
  {
    public Startup(IConfiguration configuration) => Configuration = configuration;
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options => {
          //options.ModelBinderProviders.Add(new UserModelBinderProvider());
          options.ModelBinderProviders.Insert(0, new UserModelBinderProvider());
        }); 
    }

    public void Configure(IApplicationBuilder app) =>
      app.UseDeveloperExceptionPage().UseStaticFiles().UseRouting()
        .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
   
    public static void Main(string[] args) =>
      WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
    
  }
  
}
