using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Repositories
{
    public class RepositoryCard 
    {


        MyContext myContext = new();

        public RepositoryCard(MyContext context)
        {
            this.myContext = context;
        }

        RepositoryTransaction repositoryTransaction = new();

        /// <summary>
        /// создание новой карты
        /// </summary>
        /// <param name="card"></param>
        public Card CardAdd(Guid userId)
        {
            //генерация случайных 4х значных чисел
            Random generator = new Random();
            String r1 = generator.Next(0, 10000).ToString("D4");
            String r2 = generator.Next(0, 10000).ToString("D4");
            String r3 = generator.Next(0, 10000).ToString("D4");
            String r4 = generator.Next(0, 10000).ToString("D4");
            String cardNumber = r1 + " " + r2 + " " + r3 + " " + r4;

            Card card = new Card
            {
                UserID = userId,
                CardNumber = cardNumber,
                IsActive = true
            };
            // подготовка переменной для сохранения
            myContext.Card.Add(card);

            // сохранение в бд
            myContext.SaveChanges();

            return card;
        }
        /// <summary>
        /// пополнение баланса карты
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public String Balance(int sum, string cardNumber,Guid UserId)
        {
            //FirstOrDefault() возвращает первый обект совпадающий с условием
            var Card = myContext.Card.FirstOrDefault(x=>x.CardNumber==cardNumber);
            if (Card != null)
            {
                Card.Balance += sum;
                // редактирование записи в бд
                myContext.Card.Update(Card);
                //сохранение в бд
                myContext.SaveChanges();
                //запись транзакции в бд
                repositoryTransaction.TransactionsReceipts(UserId,sum,cardNumber);
                return "баданс карты " + cardNumber + " пополнен на сумму " + sum.ToString();
            }
            else return "такой карты не существует";

        }
        /// <summary>
        /// блокировка карты
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public String BlockCard(string cardNumber, Guid userId) 
        {
            var Card = myContext.Card.FirstOrDefault(x => x.CardNumber == cardNumber && x.UserID == userId);
            if (Card != null && Card.IsActive==true) 
            {
                Card.IsActive = false;

                // редактирование записи в бд
                myContext.Card.Update(Card);
                //сохранение в бд
                myContext.SaveChanges();

                return "карта " + cardNumber + " заблокирована";
            }
            else if(Card!=null && Card.IsActive == false)

                return "карта уже заблокированна"; 

            else 
                return "проверьте номер карты";
        }
        /// <summary>
        /// разблокировка карты
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public String UnBlockCard(string cardNumber, Guid userId)
        {
            var Card = myContext.Card.FirstOrDefault(x => x.CardNumber == cardNumber && x.UserID == userId);
            if (Card != null && Card.IsActive == false)
            {
                Card.IsActive = true;
                // редактирование записи в бд
                myContext.Card.Update(Card);
                //сохранение в бд
                myContext.SaveChanges();

                return "карта " + cardNumber + " разблокирована";
            }
            else if (Card != null && Card.IsActive == true)
             
                return "карта не была заблоктрована"; 
           
            else 
                return "проверьте номер карты";
        }
        /// <summary>
        /// установка дневного лимита
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string DailyLimi(string cardNumber, Guid userId, decimal sum)
        {
            var Card = myContext.Card.FirstOrDefault(x => x.CardNumber == cardNumber && x.UserID == userId);
            if (Card != null)
            {
                Card.DailyLimit = sum;
                // редактирование записи в бд
                myContext.Card.Update(Card);
                //сохранение в бд
                myContext.SaveChanges();

                return "дневной лимит установлен, в размере " + sum.ToString();
            }

            return "карты с таки номером не найдено";
        }

        /// <summary>
        /// оплата по карте
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public string Pay(Guid userId, int sum, string cardNumber)
        {
            var Card = myContext.Card.FirstOrDefault(x => x.UserID == userId && x.CardNumber == cardNumber);

            if (Card.IsActive == false)
                return "карта заблокирована";

           else if (Card.Balance < sum)

                return "недостаточо средств";

            else if (Card.DailyLimit < (repositoryTransaction.GetTransactions(userId,cardNumber) + sum) && Card.DailyLimit > 0)

                return "вами будет превышен дневной лимит";

            else
            {
                Card.Balance -= sum;

                // редактирование записи в бд
                myContext.Card.Update(Card);
                //сохранение в бд
                myContext.SaveChanges();

                //сохранение транзакции в бд
                repositoryTransaction.TransactionsPay(userId,sum,cardNumber);

                return null;
            }
          
        }
        /// <summary>
        /// перевод по номеру карты
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        /// <param name="fromСardNumber"></param>
        /// <param name="inCardNumber"></param>
        /// <returns></returns>
        public string Remittance(Guid userId, int sum, string fromСardNumber, string inCardNumber) 
        {
            var FromCard = myContext.Card.FirstOrDefault(x => x.UserID == userId && x.CardNumber == fromСardNumber);
            var InCard = myContext.Card.FirstOrDefault(x => x.CardNumber == inCardNumber);


            if (FromCard.IsActive == false)
                return "карта заблокирована";

            else if (FromCard.Balance < sum)

                return "недостаточо средств";

            else if (FromCard.DailyLimit < (repositoryTransaction.GetTransactions(userId,fromСardNumber) + sum) && FromCard.DailyLimit > 0)

                return "вами будет превышен дневной лимит";

            else if (InCard.IsActive == false)
                return "карта на которую вы хотите совершить перевод заблокирована";

            else
            {
                FromCard.Balance -= sum;
                InCard.Balance += sum;

                // редактирование записи в бд
                myContext.Card.Update(FromCard);
                myContext.Card.Update(InCard);
                //сохранение в бд
                myContext.SaveChanges();

                //сохранение транзакции в бд
                repositoryTransaction.TransactionsReceipts(userId, sum,inCardNumber);

                return "перевод выполнен успешно";
            }
        }
    }
}
