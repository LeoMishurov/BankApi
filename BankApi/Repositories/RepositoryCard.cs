using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Repositories
{
    public class RepositoryCard 
    {   
        public RepositoryCard(MyContext context)
        {
            this.myContext = context;
        }

        MyContext myContext = new();
        RepositoryTransaction repositoryTransaction = new();

        /// <summary>
        /// создание новой карты
        /// </summary>
        /// <param name="card"></param>
        public Card CardAdd(Card card)
        {
            // подготовка переменной для сохранения
            myContext.Card.Add(card);
            // сохранение в бд
            myContext.SaveChanges();
            return card;
        }

        /// <summary>
        /// обновление данных карты
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void UpdateCard(Card card)
        {         
                // редактирование записи в бд
                myContext.Card.Update(card);
                //сохранение в бд
                myContext.SaveChanges();
        }

        /// <summary>
        /// возвращает карту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public Card GetCard(Guid userId, string cardNumber) 
        {
            Card card = new();
            card = myContext.Card.FirstOrDefault(x => x.UserID == userId && x.CardNumber == cardNumber);
            return card;
        }     
     
        /// <summary>
        /// возвращает все карты пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CardDTO>>ReturnCards(Guid userId) 
        {                 
           return await myContext.Card.Where(x => x.UserID == userId)
                .Select(x=> CardDTO.FromCard(x))
                .ToListAsync();
        }
    }
}
