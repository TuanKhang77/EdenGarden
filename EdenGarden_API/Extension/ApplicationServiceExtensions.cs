using EdenGarden_API.Data;
using EdenGarden_API.Helper;
using EdenGarden_API.Repository;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services;
using EdenGarden_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EdenGarden_API.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                //opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                //opt.UseNpgsql("Host=viaduct.proxy.rlwy.net;Database=railway;Username=postgres;Password=mpTVsYDgWwaiCnUhargJSjlNNzgPJQEM;Port=35406;SSL Mode=Prefer");
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IBookedRoomService, BookedRoomService>();
            services.AddScoped<IStatisticalService, StatisticalService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddSingleton<WebSocketService>();

            // Cloudinary service
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            return services;
        }

        //private static string BuildConnectionString()
        //{
        //    return $"Host={Environment.GetEnvironmentVariable("RAILWAY_TCP_PROXY_DOMAIN")};" +
        //           $"Database={Environment.GetEnvironmentVariable("PGDATABASE")};" +
        //           $"Username={Environment.GetEnvironmentVariable("PGUSER")};" +
        //           $"Password={Environment.GetEnvironmentVariable("PGPASSWORD")};" +
        //           $"Port={Environment.GetEnvironmentVariable("RAILWAY_TCP_PROXY_PORT")};" +
        //           "SSL Mode=Prefer";
        //}
    }
}
