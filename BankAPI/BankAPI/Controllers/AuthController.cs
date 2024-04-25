using BankAPI.CrossCutting.AppModelDtos;
using BankAPI.CrossCutting.Authentication;
using BankAPI.CrossCutting.Enumerators;
using BankAPI.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly static string INTERNAL_ERROR = "Internal server error";
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtProvider jwtProvider;

        public AuthController(UserManager<ApplicationUser> userMana, RoleManager<ApplicationRole> rolMana, IJwtProvider jwtProv)
        {
            userManager = userMana;
            roleManager = rolMana;
            jwtProvider = jwtProv;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateRole([FromBody] RolDTO dto)
        {
            try
            {
                var rol = new ApplicationRole { Name = dto.Name };
                var newRol = await roleManager.CreateAsync(rol);

                return StatusCode(StatusCodes.Status200OK, new JsonResponse
                {
                    Status = StatusCodes.Status200OK,
                    Msg = $"Rol {dto.Name} creado!",
                    Result = newRol
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Msg = $"[ERROR]: {INTERNAL_ERROR}",
                    Errors = new string[] { ex.Message, ex.ToString() },
                });
            }

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login([FromBody] AuthDTO data)
        {

            var user = await userManager.FindByEmailAsync(data.UserName);

            if (user != null && user.Status.Equals(StatesEnum.Active))
            {
                bool isValid = await userManager.CheckPasswordAsync(user, data.Password);

                if (isValid)
                {
                    data.Id = user.Id.ToString();
                    data.FullName = user.Person.FullName;
                    data.Password = string.Empty;
                    data.Token = jwtProvider.Generate(data);

                    return StatusCode(StatusCodes.Status200OK, new JsonResponse { Status = StatusCodes.Status200OK, Result = data });
                }
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResponse
            {
                Status = StatusCodes.Status400BadRequest,
                Msg = "Credenciales invalidas",
                Result = data
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO data)
        {

            try
            {
                var user = await userManager.FindByEmailAsync(data.UserName);
                if (user != null)
                    return StatusCode(StatusCodes.Status200OK, new JsonResponse { Status = StatusCodes.Status400BadRequest, Msg = "El usuario ya existe" });

                //string passHash = passwordHasher.Hash(data.Password);
                user = new ApplicationUser
                {
                    Email = data.UserName,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    UserName = data.UserName,
                    EmailConfirmed = true,
                    Status = StatesEnum.Active,
                    Person = new Person
                    {
                        FullName = data.Person.FullName,
                        DocNumber = data.Person.DocNumber,
                        DocumentType = data.Person.DocumentType,
                        Phone = data.Person.Phone,
                        AddressList = []
                    }
                };

                var roles = await roleManager.FindByNameAsync(RolesEnum.CLIENT);
                if (roles == null)
                    return StatusCode(StatusCodes.Status200OK, new JsonResponse
                    {
                        Status = StatusCodes.Status200OK,
                        Msg = $"Debes crear el rol {RolesEnum.CLIENT}",
                        Result = data
                    });

                var result = await userManager.CreateAsync(user, data.Password);
                if (result.Succeeded)
                {
                    _ = userManager.AddToRoleAsync(user, RolesEnum.CLIENT);
                    return StatusCode(StatusCodes.Status200OK, new JsonResponse
                    {
                        Status = StatusCodes.Status200OK,
                        Msg = "Usuario creado correctamente!",
                        Result = data
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new JsonResponse
                {
                    Status = StatusCodes.Status400BadRequest,
                    Msg = "Error creando el usuario",
                });
            }
            catch (Exception ex)
            {
                return StatusCodeError(ex);
            }
        }

        #region STATUSCODEERROR
        private ObjectResult StatusCodeError(Exception ex)
        {
            return StatusCode(
                     StatusCodes.Status500InternalServerError,
                     new JsonResponse
                     {
                         Status = StatusCodes.Status500InternalServerError,
                         Msg = INTERNAL_ERROR,
                         Errors = new string[] { ex.Message, ex.ToString() }
                     }
                 );
        }

        #endregion
    }
}
