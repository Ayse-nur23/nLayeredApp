using Business;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using DataAccess;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddBusinessServices();
            builder.Services.AddDataAccessServices(builder.Configuration);

            builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => { p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

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

            if(app.Environment.IsProduction())
                app.ConfigureCustomExceptionMiddleware();
            app.UseAuthorization();


            app.MapControllers();

            app.UseCors(opt => opt.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.Run();
        }
    }
}