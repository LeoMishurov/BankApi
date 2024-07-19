namespace BankClient.Model
{
    public class CardDTO
    {
        public string CardNumber { get; set; }
        public string ExpirationCard { get; set; }
        public decimal Balance { get; set; }
        public decimal DailyLimit { get; set; }
        public bool IsActive { get; set; }
    }

}
