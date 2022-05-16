using DAL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerApplicationLayer;
using ServerApplicationLayerImplementation.Services;

namespace ServerApplicationLayerImplementation;
public static class SALImpModule
{
  public static void Register ( this WebApplicationBuilder builder )
  {
    builder.RegisterDAL ();
    var services = builder.Services;
    services.AddFluentValidation (x =>
    {
      x.DisableDataAnnotationsValidation = true;
    });
    services.AddScoped<IJwtManager, JwtManager> ();
    services.AddScoped<AuthFacade, AuthFacadeImpl> ();
  }
}
