using App.Business.Exceptions.BlogExceptions;
using App.Business.Exceptions.Common;
using App.Business.Services.Interfaces;
using App.Business.ViewModels.BlogVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly IBlogService _serBlog;
        private readonly IWebHostEnvironment _env;

        public BlogController(IBlogService serBlog, IWebHostEnvironment env)
        {
            _serBlog = serBlog;
            _env = env;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Table()
        {
            string returnUrl = HttpContext.Request.Query["returnUrl"];
            if(returnUrl is not null) return RedirectToAction("Login", "Account");

            ViewData["Blogs"] = await _serBlog.GetAllAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var oldBlog = await _serBlog.GetByIdAsync(id);

                return View(oldBlog);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (BlogNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serBlog.DeleteAsync(id);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (BlogNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVM blog)
        {
           
            try
            {
                await _serBlog.CreateAsync(blog, _env.WebRootPath);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (BlogNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (ObjectRequiredException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (BlogImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var oldBlog = await _serBlog.GetByIdAsync(id);
                UpdateBlogVM vm = new()
                {
                    Title = oldBlog.Title,
                    Description = oldBlog.Description,
                };

                return View(vm);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (BlogNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVM blog)
        {
            try
            {
                await _serBlog.UpdateAsync(blog, _env.WebRootPath);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (BlogNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (ObjectRequiredException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (BlogImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            
        }
    }
}
