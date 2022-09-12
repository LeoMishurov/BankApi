namespace BankApi.Models
{
    public class Card
    {
        public int Id { get; set; }

        public Guid UserID { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationCard { get; set; } = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, 1);
        public decimal Balance { get; set; }
        public decimal DailyLimit { get; set; }
        public bool IsActive { get; set; }

        //Сылка на User для enti framevork
        public User User { get; set; }

    }
    public class CardDTO 
    {
        public string CardNumber { get; set; }
        public string ExpirationCard { get; set; }
        public decimal Balance { get; set; }
        public decimal DailyLimit { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// присваивает значения полям CardDTO и возвращает заполненный экземпляр класса CardDTO
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static CardDTO FromCard(Card card)
        => new CardDTO
        {
            CardNumber = card.CardNumber,
            ExpirationCard = card.ExpirationCard.ToString("MM/yyyy"),
            Balance = card.Balance,
            DailyLimit = card.DailyLimit,
            IsActive = card.IsActive
        };
    }
  
}
