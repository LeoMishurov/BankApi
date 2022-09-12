using BankApi.Models;
using BankApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi;

namespace BankApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly RepositoryCard repositoryCard;

        public CardController(RepositoryCard repositoryCard)
        {
            this.repositoryCard = repositoryCard;
        }

        /// <summary>
        /// выпуск новой карты
        /// </summary>
        /// <returns></returns>
        [HttpPost("Save")]
        public ActionResult<CardDTO> SaveCard()
        {
            // Достает id пользоввателя из токина
            var userId = User.Identity.GetId();

            return CardDTO.FromCard(repositoryCard.CardAdd(userId));
        }
        /// <summary>
        /// возвращает все карты пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("ReturnCards")]
        public ActionResult<List<CardDTO>> ReturnCards()
        {
           
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.ReturnCards(UserId);
        }

        /// <summary>
        /// пополнение баланса карты
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost("Balance")]
        public ActionResult<String> Balance(int sum, string cardNumber)
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.Balance(sum, cardNumber, UserId);
        }

        /// <summary>
        /// блокировка карты
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost("Block")]
        public ActionResult<String> BlockCard(string cardNumber)
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.BlockCard(cardNumber, UserId);
        }

        /// <summary>
        /// разблокировка карты
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost("UnBlock")]
        public ActionResult<String> UnBlockCard(string cardNumber)
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.UnBlockCard(cardNumber, UserId);
        }

        /// <summary>
        /// установка дневного лимита
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost("DailyLimit")]
        public ActionResult<String> DailyLimit( decimal sum, string cardNumber)
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.DailyLimit(cardNumber, UserId, sum);           
        }

        /// <summary>
        /// оплата по карте
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost("Pay")]
        public ActionResult<String> Pay(int sum, string cardNumber)
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            var error = repositoryCard.Pay(UserId, sum, cardNumber);

            if (string.IsNullOrEmpty(error))
                return Ok();
            else
                return BadRequest(error);
        }

        /// <summary>
        /// перевод по номеру карты
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="fromCardNumber"></param>
        /// <param name="inCardNumber"></param>
        /// <returns></returns>
        [HttpPost("Remittance")]
        public ActionResult<String> Remittance(int sum, string fromCardNumber, string inCardNumber)
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            var error = repositoryCard.Remittance(UserId, sum, fromCardNumber, inCardNumber);

            if (string.IsNullOrEmpty(error))
                return Ok();
            else
                return BadRequest(error);
        }
    }
}
