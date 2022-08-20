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

        [HttpPost("Save")]
        public ActionResult<CardDTO> SaveCard()
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();
       
            return CardDTO(repositoryCard.CardAdd(UserId));
        }

        [HttpPost("Balance")]
        public ActionResult<String> Balance(int sum, string cardNumber) 
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.Balance(sum, cardNumber, UserId);
        }

        [HttpPost("Block")]
        public ActionResult<String> BlockCard(string cardNumber)
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.BlockCard(cardNumber, UserId);
        }

        [HttpPost("UnBlock")]
        public ActionResult<String> UnBlockCard(string cardNumber)
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.UnBlockCard(cardNumber, UserId);
        }

        [HttpPost("DailyLimit")]
        public ActionResult<String> DailyLimit(string cardNumber, decimal sum)
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.DailyLimi(cardNumber, UserId, sum);
        }

        [HttpPost("Pay")]
        public ActionResult<String> Pay(int sum, string cardNumber) 
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            var error = repositoryCard.Pay(UserId, sum, cardNumber);
            if (string.IsNullOrEmpty(error))
                return Ok();
            else
                return BadRequest(error);
        }

        [HttpPost("translation")]
        public ActionResult<String> Remittance(int sum, string fromCardNumber, string inCardNumber)
        {
            //достает id пользоввателя из токина
            var UserId = User.Identity.GetId();

            return repositoryCard.Remittance(UserId, sum, fromCardNumber, inCardNumber);
        }
        private CardDTO CardDTO(Card card) 
        {
            CardDTO cardDTO = new CardDTO
            {
                CardNumber = card.CardNumber,
                ExpirationCard = card.ExpirationCard.ToString("MM/yy")
            };
            return cardDTO;
        }
    }
}
