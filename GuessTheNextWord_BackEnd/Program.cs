
using GuessTheNextWord_BackEnd.Services;

namespace GuessTheNextWord_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<Data.AppDbContext>();

            builder.Services.AddScoped<Repositories.Interfaces.IGameRepository, Repositories.GameRepository>();
            builder.Services.AddScoped<Repositories.Interfaces.IPlayerRepository, Repositories.PlayerRepository>();
            builder.Services.AddScoped<Repositories.Interfaces.IGamePlayersRepository, Repositories.GamePlayerRepository>();
            builder.Services.AddScoped<Repositories.Interfaces.IGameWordRepository, Repositories.GameWordRepository>();
            builder.Services.AddSingleton<WordLookUp>();

          

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
