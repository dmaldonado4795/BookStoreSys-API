using BookStoreSys_API.Domain.Services;
using BookStoreSys_API.Domain.Services.Impl;
using BookStoreSys_API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreSys_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<INationalityService, NationalityServiceImpl>();
            builder.Services.AddScoped<IAuthorService, AuthorServiceImpl>();
            builder.Services.AddScoped<IGenreService, GenreServiceImpl>();
            builder.Services.AddScoped<IBookService, BookServiceImpl>();

            builder.Services.AddControllers();
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
