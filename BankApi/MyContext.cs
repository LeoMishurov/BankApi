using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace BankApi.Models
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
        }

        public MyContext(DbContextOptions<MyContext> options): base(options)
        {
        }

        public DbSet<Card> Card { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set;}

        /// <summary>
        /// настройки подключения к базе данных
        /// </summary>
        /// <param name="optionsBuilder"></param>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source = C:/Users/leont/Desktop/Work/BankApi/BankApi.db;");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5435;Database=postgres;Username=postgres;Password=123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Bank");
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=123")
        //            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
