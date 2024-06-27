namespace BankApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public Guid UserID { get; set; }

        public int SumPay { get; set; }

        public int receipts { get; set; }

        public string CardNumber { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow.AddYears(1).Date.AddHours(DateTime.UtcNow.Hour).AddMinutes(DateTime.UtcNow.Minute);

        // Сылка на User для EF
        public User User { get; set; }
    }
}
