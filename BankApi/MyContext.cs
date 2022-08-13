using Microsoft.EntityFrameworkCore;


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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = C:/Users/leont/Desktop/Work/BankApi/BankApi.db;");
        }
    }
}
