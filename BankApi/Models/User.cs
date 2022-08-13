namespace BankApi.Models
{
    public class User
    {
       public Guid Id { get; set; }
       public string Login { get; set; }
       public string Password { get; set; }

        //Сылка на Card для enti framevork
        public List<Card> Cards { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
  
}
