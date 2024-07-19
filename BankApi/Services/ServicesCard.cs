using BankApi.Models;
using BankApi.Repositories;

namespace BankApi.Services
{
    public class ServicesCard

    {
        private readonly RepositoryCard repositoryCard;
        private readonly RepositoryTransaction repositoryTransaction; 
        public ServicesCard(RepositoryCard repositoryCard, RepositoryTransaction repositoryTransaction)
        {
            this.repositoryCard = repositoryCard;
            this.repositoryTransaction = repositoryTransaction;
        }

        /// <summary>
        /// Создание новой карты пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
            return repositoryCard.CardAdd(card);
        }

        /// <summary>
        /// пополнение баланса карты
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public String Balance(int sum, string cardNumber, Guid userId)
        {
            //находим нашу карту в базе
            var card = repositoryCard.GetCard(userId, cardNumber);
            if (card != null)
            {
                card.Balance += sum;
               
              repositoryTransaction.TransactionsReceipts(userId, sum, cardNumber);
              //Обновление данных по карте
              repositoryCard.UpdateCard(card);
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
            //находим нашу карту в базе
            var card = repositoryCard.GetCard(userId, cardNumber);

            if (card != null && card.IsActive == true)
            {
                card.IsActive = false;

                //Обновление данных по карте
                repositoryCard.UpdateCard(card);

                return "карта " + cardNumber + " заблокирована";
            }
            else if (card != null && card.IsActive == false)

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
            //находим нашу карту в базе
            var card = repositoryCard.GetCard(userId, cardNumber);

            if (card != null && card.IsActive == false)
            {
                card.IsActive = true;
                //Обновление данных по карте
                repositoryCard.UpdateCard(card);

                return "карта " + cardNumber + " разблокирована";
            }
            else if (card != null && card.IsActive == true)

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
        public string DailyLimit(string cardNumber, Guid userId, decimal sum)
        {
            //находим нашу карту в базе
            var card = repositoryCard.GetCard(userId, cardNumber);

            if (card != null)
            {
                card.DailyLimit = sum;

                //Обновление данных по карте
                repositoryCard.UpdateCard(card);

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
            //находим нашу карту в базе
            var card = repositoryCard.GetCard(userId, cardNumber);

            if (card.IsActive == false)
                return "карта заблокирована";

            else if (card.Balance < sum)

                return "недостаточо средств";

            else if (card.DailyLimit < (repositoryTransaction.GetTransactions(cardNumber) + sum) && card.DailyLimit > 0)

                return "вами будет превышен дневной лимит";

            else
            {
                card.Balance -= sum;

                //Обновление данных по карте
                repositoryCard.UpdateCard(card);

                // Сохранение транзакции в бд
                repositoryTransaction.TransactionsPay(userId, sum, cardNumber);

                return "";
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
            var fromCard = repositoryCard.GetCard(userId, fromСardNumber);
            var inCard = repositoryCard.GetCard(userId, inCardNumber);


            if (fromCard.IsActive == false)
                return "карта заблокирована";

            else if (fromCard.Balance < sum)

                return "недостаточо средств";

            else if (fromCard.DailyLimit < (repositoryTransaction.GetTransactions(fromСardNumber) + sum) && fromCard.DailyLimit > 0)

                return "вами будет превышен дневной лимит";

            else if (inCard.IsActive == false)
                return "карта на которую вы хотите совершить перевод заблокирована";

            else
            {
                fromCard.Balance -= sum;
                inCard.Balance += sum;

                //Обновление данных по карте
                repositoryCard.UpdateCard(fromCard);
                repositoryCard.UpdateCard(inCard);

                // Сохранение транзакции в бд
                repositoryTransaction.TransactionsReceipts(userId, sum, inCardNumber);

                return "";
            }
        }
    }
}
