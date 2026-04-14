
using JobListingsAPI.Data;
using JobListingsAPI.Filters;
using JobListingsAPI.Middlewares;
using JobListingsAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace JobListingsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("JobListingsDb"));

          
            builder.Services.AddScoped<IJobService, JobService>();


            builder.Services.AddScoped<ValidateJobFilter>();


            var app = builder.Build();

            app.UseExceptionHandler("/error");

            app.UseMiddleware<RequestLoggerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Map("/error", (HttpContext ctx) =>
            {
                ctx.Response.StatusCode = 500;
                return Results.Problem("An unexpected error occurred.");
            });

            app.Run();
        }
    }
}
