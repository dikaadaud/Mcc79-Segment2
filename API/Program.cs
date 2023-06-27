using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Configurasinya ke Database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(connectionString));

            // add Repository to HTTP 
            builder.Services.AddScoped<IUniversityRepository, UniverisityRepository>();
            builder.Services.AddScoped<IRoom, RoomRepository>();
            builder.Services.AddScoped<IRolesRepository, RoleRepository>();
            builder.Services.AddScoped<IAccountRole, AccountRoleRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IEducationRepository, EducationRepository>();

            //add Service to Htp
            builder.Services.AddScoped<UniversityService>();
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<RoomService>();
            builder.Services.AddScoped<AccountRoleService>();
            builder.Services.AddScoped<EmployeeeService>();
            builder.Services.AddScoped<BookingService>();
            builder.Services.AddScoped<EducationService>();
            builder.Services.AddScoped<AccountService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}