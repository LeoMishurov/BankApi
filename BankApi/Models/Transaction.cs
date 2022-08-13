namespace BankApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public Guid UserID { get; set; }

        public int SumPay { get; set; }

        public int receipts { get; set; }

        public string CardNumber { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.Date;

        //Сылка на User для enti framevork
        public User User { get; set; }
    }
}
