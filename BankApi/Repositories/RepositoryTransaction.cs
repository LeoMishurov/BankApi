using BankApi.Models;
namespace BankApi.Repositories

{
    public class RepositoryTransaction
    {
        MyContext myContext;

        public RepositoryTransaction(MyContext context)
        {
            this.myContext = context;
        }

        /// <summary>
        /// сохранение транзакции (расходов) в бд
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        public void TransactionsPay(Guid userId, int sum, string cardNumber)
        {
            Transaction transaction = new Transaction
            {
                UserID = userId,
                SumPay = sum,
                CardNumber = cardNumber
            };
            // Подготовка переменной для сохранения
            myContext.Transaction.Add(transaction);
            // Сохранение в бд
            myContext.SaveChanges();
        }

        /// <summary>
        /// сохранение транзакции (поступлений) в бд
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        public void TransactionsReceipts(Guid userId, int sum, string cardNumber)
        {
            Transaction transaction = new Transaction
            {
                UserID = userId,
                receipts = sum,
                CardNumber = cardNumber
            };
            // Подготовка переменной для сохранения
            myContext.Transaction.Add(transaction);
            // Сохранение в бд
            myContext.SaveChanges();
        }

        /// <summary>
        /// сумма расходов за сегодняшний день
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTransactions(string cardNumber)
        {
            return myContext.Transaction
             .Where(x => x.Date == DateTime.UtcNow.Date && x.CardNumber == cardNumber)
             .Sum(x => x.SumPay);
        }
    }
}
