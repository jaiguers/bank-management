using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Business.Services;
using BankAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    /// <summary>
    /// Controlador para las transacciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AmountController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public AmountController(BankDbContext db)
        {
            transactionService = new TransactionService(db);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> MakeDeposit([FromBody] AmountDTO amount)
        {
            try
            {
                var card = transactionService.CreateTransaction(amount);

                return StatusCode(StatusCodes.Status200OK, new JsonResponse
                {
                    Status = StatusCodes.Status200OK,
                    Msg = "Transacción realizada correctamente",
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
                         Msg = "No se pudo realizar la transacción",
                         Errors = new string[] { ex.Message, ex.ToString() }
                     }
                 );
            }

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> MakeWithdrawal([FromBody] AmountDTO amount)
        {
            try
            {
                var card = transactionService.CreateTransaction(amount);

                return StatusCode(StatusCodes.Status200OK, new JsonResponse
                {
                    Status = StatusCodes.Status200OK,
                    Msg = "Retiro realizado correctamente!",
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
                         Msg = "No se pudo realizar el retiro",
                         Errors = new string[] { ex.Message, ex.ToString() }
                     }
                 );
            }
        }
    }
}
