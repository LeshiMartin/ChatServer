using DataAccessLayerAbstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;
public static class DALModule
{
  public static void RegisterDAL ( this WebApplicationBuilder builder )
  {
    var config = builder.Configuration;
    var configPath = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "DALSettings.json");
    config.AddJsonFile (configPath);
    var services = builder.Services;
    var connString = config[ "ConnectionStrings:default" ];
    services.AddDbContextFactory<AppDbContext> (x => x.UseNpgsql (connString));
    services.AddScoped<IUserRepository, UserRepository> ();
    services.AddScoped<IMessageRepository, MessageRepository> ();
  }
}
