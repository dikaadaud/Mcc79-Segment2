using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TokenHandler = API.Ultilities.Handler.TokenHandler;

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

            //handler
            builder.Services.AddScoped<ITokenHandler, TokenHandler>();
            builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler
                (
                    builder.Configuration["EmailService:SmtpServer"],
                    int.Parse(builder.Configuration["EmailService:SmtpPort"]),
                    builder.Configuration["EmailService:FromEmailAddress"]
                ));

            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            // Jwt Configuration
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false; // For development
                       options.SaveToken = true;
                       options.TokenValidationParameters = new TokenValidationParameters()
                       {
                           ValidateIssuer = true,
                           ValidIssuer = builder.Configuration["JWTService:Issuer"],
                           ValidateAudience = true,
                           ValidAudience = builder.Configuration["JWTService:Audience"],
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:Key"])),
                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.Zero
                       };
                   });


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