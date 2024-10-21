using EdenGarden_API.Data;
using EdenGarden_API.Extension;
using EdenGarden_API.Helper;

using EdenGarden_API.Models.Entities;
using EdenGarden_API.Services;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Configuration.AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.Production.json");
builder.Services.AddAplicationServices(builder.Configuration);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
    (options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vi toi qua dep trai nen ko ai yeu toi nen vi the toi van co don giua chon khong nguoi canh ben")),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });


builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy(Policies.Staff, policy => policy.RequireClaim(Policies.Staff));
    // options.AddPolicy(Policies.Admin, policy => policy.RequireClaim(Policies.Admin));
    options.AddPolicy(Policies.Admin, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin"); 
    });
    
});


//builder.Services.AddMvc().AddNewtonsoftJson(
//          options =>
//          {
//              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//          });

// For deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8080"));
app.UseWebSockets();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/ws", async context =>
    {
        var webSocketService = context.RequestServices.GetRequiredService<WebSocketService>();
        await webSocketService.HandleWebSocketAsync(context);
    });
});
    

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
