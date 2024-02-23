using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPay.Core;
using RPay.Dtos;

namespace RPay.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardService _service;

        public CardController(ILogger<CardController> logger, ICardService cardService)
        {
            _logger = logger;
            _service = cardService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCard(CreateCard card)
        {
            var response = await _service.CreateCard(card);
            if (!response.Success)
            {
                return new BadRequestObjectResult(response.ErrorMessage);
            }

            return new OkObjectResult(response.Data);
        }

        [HttpPut("pay/{cardId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PayCard(Guid cardId, decimal amount)
        {
            var response = await _service.PayCard(cardId, amount);
            if (!response.Success)
            {
                return new BadRequestObjectResult(response.ErrorMessage);
            }
            return new OkObjectResult(response.Data);
        }

        [HttpGet("balance/{cardId}")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCardBalance(Guid cardId)
        {
            _logger.LogDebug("Getting balance for card: {cardId}", cardId);
            var response = await _service.GetCardBalance(cardId);
            if (!response.Success)
            {
                return new NotFoundObjectResult(response.ErrorMessage);
            }

            return new OkObjectResult(response.Data);
        }
    }
}