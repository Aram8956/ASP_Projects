using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite; // Добавьте эту директиву using
using Project.Repositories;
using Project.Data;
using Project.Services;

namespace ProjMain
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            var connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=hotel.db";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

            // Repositories
            builder.Services.AddScoped<IHotel, HotelRep>();
            builder.Services.AddScoped<IRoom, RoomRep>();
            builder.Services.AddScoped<IUser, UserRep>();
            builder.Services.AddScoped<IProfile, ProfileRep>();
            builder.Services.AddScoped<IBooking, BookingRep>();

            // Services
            builder.Services.AddScoped<IHotelServ, HotelServ>();
            builder.Services.AddScoped<IRoomServ, RoomService>();
            builder.Services.AddScoped<IUserServ, UserServ>();
            builder.Services.AddScoped<IProfileServ, ProfileServ>();
            builder.Services.AddScoped<IBookingServ, BookingServ>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();

        }
    }
}
