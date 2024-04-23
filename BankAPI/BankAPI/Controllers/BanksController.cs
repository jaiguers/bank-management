using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Business.Services;
using BankAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService bankService;
        public BanksController(BankDbContext dbContext)
        {
            bankService = new BankService(dbContext);
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult GetAll()
        {
            var banks = bankService.GetAllBanks();

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
