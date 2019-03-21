using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace Api.Controllers
{
    public class AuthenticateController : ApiController
    {
        private readonly IUserService _userService;

        public AuthenticateController()
        {
            
        }

        public AuthenticateController(IUserService service)
        {
            SetInitialData();
            _userService = service;
        }

        [HttpPost]
        [Route("Api/Authentication/Register")]
        public async Task<IHttpActionResult> Register(UserDTO user)
        {
            
            //if (!ModelState.IsValid)
                //return BadRequest();
            try
            {
                await _userService.Create(user);
                return Ok();
            }
            catch (Exception )
            {
                return BadRequest();
            }
        }

        public async Task<IHttpActionResult> Login(UserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _userService.Authenticate(user);
            if(result==null)
                ModelState.AddModelError("", "Неверный логин или пароль.");
            var context = HttpContext.Current.GetOwinContext().Authentication;
            context.SignOut();
            context.SignIn(new AuthenticationProperties()
            {
                IsPersistent = true

            },result);
            return Ok();
        }

        private void SetInitialData()
        {
             _userService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "123",
                Name = "Семен Семенович Горбунков",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }




    }
}
