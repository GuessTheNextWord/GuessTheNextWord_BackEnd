using GuessTheNextWord_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessTheNextWord_BackEnd.Data
{
    public class AppDbContext : DbContext   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Game> Games { get; set; }
        public DbSet<Models.Player> Players { get; set; }
        public DbSet<Models.Word> Words { get; set; }
        public DbSet<Models.GamePlayer> GamePlayers { get; set; }
        public DbSet<Models.GameWord> GameWords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.GamePlayer>()
                .HasKey(gp => new { gp.GameId, gp.PlayerId });
            modelBuilder.Entity<Models.Player>()
                .Property(x => x.State)
                .HasConversion<string>();
            modelBuilder.Entity<Models.Game>()
                .Property(x => x.State)
                .HasConversion<string>();
            modelBuilder.Entity<GamePlayer>()
                .HasOne(pg => pg.Game)
                .WithMany(g => g.GamePlayers)
                .HasForeignKey(pg => pg.GameId);

            modelBuilder.Entity<GamePlayer>()
                .HasOne(pg => pg.Player)
                .WithMany(p => p.Games)
                .HasForeignKey(pg => pg.PlayerId);

            modelBuilder.Entity<Models.GameWord>()
                .HasKey(gw => new { gw.GameId, gw.WordId });
            modelBuilder.Entity<GameWord>()
                .HasOne(pg => pg.Word)
                .WithMany(g => g.Games)
                .HasForeignKey(pg => pg.WordId);
            modelBuilder.Entity<GameWord>()
                .HasOne(pg => pg.Game)
                .WithMany(p => p.Words)
                .HasForeignKey(pg => pg.GameId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DANISLAV-LAPTOP;Database=GuessTheNextWordDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
