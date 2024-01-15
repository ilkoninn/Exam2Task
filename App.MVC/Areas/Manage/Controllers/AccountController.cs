using App.Business.Exceptions.AccountExceptions;
using App.Business.Services.Interfaces;
using App.Business.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace App.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly IAccountService _ser;

        public AccountController(IAccountService ser)
        {
            _ser = ser;
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _ser.CreateRoles();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                await _ser.Register(register);

                return RedirectToAction(nameof(Login));
            }
            catch (SameEmailUserException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            try
            {
                await _ser.Login(login);

                return RedirectToAction("Index", "Admin");
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _ser.Logout();

            return RedirectToAction(nameof(Login));
        }

    }
}
