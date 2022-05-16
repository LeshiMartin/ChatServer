using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServerApplicationLayer;
using ServerApplicationLayer.Dtos.AuthDtos;
using ServerApplicationLayer.Exceptions;
using ServerApplicationLayerImplementation;
using System.Text;

var builder = WebApplication.CreateBuilder (args);
builder.Register ();
var config = builder.Configuration;
var secret = config[ "Jwt:Key" ];
SetAuthentication (builder, secret);
var app = builder.Build ();
app.UseAuthentication ();
app.MapGet ("/", () => "Hello World!");
app.MapPost ("login", async ( LoginDto dto, AuthFacade authFacade, ILogger<Program> logger ) =>
{
  try
  {
    var tokenSource = new CancellationTokenSource (TimeSpan.FromMinutes (1));
    var response = await authFacade.LoginAsync (dto, tokenSource.Token);
    return Results.Ok (response);
  }
  catch ( WrongPasswordException exc )
  {
    return Results.BadRequest (exc.Message);
  }
  catch ( UserNotFoundException exc )
  {
    return Results.BadRequest (exc.Message);
  }
  catch ( Exception exc )
  {
    logger.LogError (exc, exc.Message);
    return Results.StatusCode (500);
  }
});

app.MapPost ("register", async ( RegisterDto dto, AuthFacade authFacade, ILogger<Program> logger ) =>
{
  try
  {
    var tokenSource = new CancellationTokenSource (TimeSpan.FromMinutes (1));
    await authFacade.RegisterAsync (dto, tokenSource.Token);
    return Results.Ok ();
  }
  catch ( UsernameTakenException exc )
  {
    return Results.BadRequest (exc.Message);
  }
  catch ( Exception exc )
  {
    logger.LogError (exc, exc.Message);
    return Results.StatusCode (500);
  }
});
app.Run ();

static void SetAuthentication ( WebApplicationBuilder builder, string secret )
{
  var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (secret));
  builder.Services.AddAuthentication (options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  })

     .AddJwtBearer (options =>
     {
       options.SaveToken = true;
       options.RequireHttpsMetadata = false;
       options.TokenValidationParameters = new TokenValidationParameters
       {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = key
       };
       options.Events = new JwtBearerEvents ()
       {
         OnMessageReceived = context =>
         {
           var accessToken = context.Request.Query[ "access_token" ];
           var path = context.HttpContext.Request.Path;
           if ( !string.IsNullOrEmpty (accessToken) )
             context.Token = accessToken;

           return Task.CompletedTask;
         },
       };
     });
}