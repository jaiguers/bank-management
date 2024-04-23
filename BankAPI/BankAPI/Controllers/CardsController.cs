using BankAPI.CrossCutting.AppModelDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public ActionResult GetCards()
        {
            List<BankDTO> banks = [];

            return StatusCode(StatusCodes.Status200OK, new JsonResponse
            {
                Status = StatusCodes.Status200OK,
                Msg = string.Empty,
                Errors = string.Empty,
                Result = banks,
            });
        }
    }
}
