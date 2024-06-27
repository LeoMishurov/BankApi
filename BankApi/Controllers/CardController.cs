using BankApi.Models;
using BankApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ToDoListApi;

namespace BankApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly RepositoryCard repositoryCard;
        private readonly IDistributedCache distributedCach;
        private readonly IMemoryCache memoryCache;

        public CardController(RepositoryCard repositoryCard, IDistributedCache distributedCach, IMemoryCache memoryCache)
        {
            this.repositoryCard = repositoryCard;
            this.distributedCach = distributedCach;
            this.memoryCache = memoryCache;
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
        [HttpGet("ReturnCards")]
        public async Task< ActionResult<List<CardDTO>>> ReturnCards()
        {     
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            //return await memoryCache.GetOrCreateAsync(UserId.ToString(), async x =>
            //{
                return await repositoryCard.ReturnCards(UserId);
            //});

        }
        [HttpGet("ReturnCards2")]
        public async Task<ActionResult<List<CardDTO>>> ReturnCards2()
        {
            // Достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            List<CardDTO> result;

            //Провнряет, есть ли катры в кэше
            result = distributedCach.GetObject<CardDTO>(UserId.ToString());

            //Если есть возвращаем
            if (result != null)
                return Ok(result);

            //Если нет заправшиваем из базы и сохраняем в кэш
            result = await repositoryCard.ReturnCards(UserId);

            //Сохраняем в кэш, время жизни обьекта 5 минут TimeSpan.FromMinutes(5)
            distributedCach.SetObject(UserId.ToString(), result, TimeSpan.FromMinutes(5));

            return Ok(result);

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
