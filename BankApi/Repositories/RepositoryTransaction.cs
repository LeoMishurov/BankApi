using BankApi.Models;
namespace BankApi.Repositories

{
    public class RepositoryTransaction
    {
        MyContext myContext = new();

        public RepositoryTransaction(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public RepositoryTransaction()
        {
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
            // подготовка переменной для сохранения
            myContext.Transaction.Add(transaction);
            // сохранение в бд
            myContext.SaveChanges();
        }

        /// <summary>
        /// сохранение транзакции (поступлений) в бд
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        public void TransactionsReceipts(Guid userId, int sum, string cardNumber)
        {
            Transaction transaction = new Transaction
            {
                UserID = userId,
                receipts = sum,
                CardNumber = cardNumber
            };
            // подготовка переменной для сохранения
            myContext.Transaction.Add(transaction);
            // сохранение в бд
            myContext.SaveChanges();
        }

        /// <summary>
        /// сумма расходов за сегодняшний день
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTransactions(Guid userId, string cardNamber) 
        {
            
              return   myContext.Transaction.Where(x => x.Date == DateTime.Now.Date && x.CardNumber == cardNamber).Sum(x => x.SumPay);
                    
            //return transactions.Sum(x => x.SumPay);
        }
    }
}
