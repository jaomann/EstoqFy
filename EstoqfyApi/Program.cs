using DotNetEnv;
using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Contracts.Services;
using EstoqFy.Database;
using EstoqFy.Repository;
using EstoqFy.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EstoqFy
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            Env.Load();
            var connectionString = $"Server=db;Port=3306;Database={Env.GetString("DB_NAME")};User Id={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")}";
            builder.Services.AddDbContext<Context>(opt =>
                opt.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 11))));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            // Configuração do logging
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                try
                {
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao inicializar banco: {ex.Message}");
                }
            }

            app.Run();
        }
    }
}
