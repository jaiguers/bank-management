using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.CrossCutting.Enumerators;
using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Business.Services;
using BankAPI.Domain.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService cardService;
        public CardsController(BankDbContext bankDbContext)
        {
            cardService = new CardService(bankDbContext);
        }

        /// <summary>
        /// Obtiene todas las tarjetas de un usuario
        /// </summary>
        /// <param name="id">Id de usuaroio</param>
        /// <returns>lista de tarjetas</returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult GetCards(string id)
        {
            var userId = Guid.Parse(id);
            var cards = cardService.GetCardByUserId(userId);

            return StatusCode(StatusCodes.Status200OK, new JsonResponse
            {
                Status = StatusCodes.Status200OK,
                Msg = string.Empty,
                Errors = string.Empty,
                Result = cards,
            });
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateCard([FromBody] CreateCardsDTO data)
        {
            try
            {
                var card = cardService.CreateCard(data);

                return StatusCode(StatusCodes.Status200OK, new JsonResponse
                {
                    Status = StatusCodes.Status200OK,
                    Msg = $"Tarjeta creada correctamente con el numero {card.CardNumber}",
                    Result = card,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                     StatusCodes.Status500InternalServerError,
                     new JsonResponse
                     {
                         Status = StatusCodes.Status500InternalServerError,
                         Msg = "No se pudo crear la tarjeta",
                         Errors = new string[] { ex.Message, ex.ToString() }
                     }
                 );
            }
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult ActivateCard(string id)
        {
            try
            {
                var card = cardService.ActivateCard(id);

                return StatusCode(StatusCodes.Status200OK, new JsonResponse
                {
                    Status = StatusCodes.Status200OK,
                    Msg = "Tarjeta activada correctamente!",
                    Result = card,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                     StatusCodes.Status500InternalServerError,
                     new JsonResponse
                     {
                         Status = StatusCodes.Status500InternalServerError,
                         Msg = "No se pudo activar la tarjeta",
                         Errors = new string[] { ex.Message, ex.ToString() }
                     }
                 );
            }
        }

    }
}
