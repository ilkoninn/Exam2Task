using App.Business.Services.Interfaces;
using App.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.MVC.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _serBlog;

        public BlogViewComponent(IBlogService serBlog)
        {
            _serBlog = serBlog;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IQueryable<Blog> blogs = await _serBlog.GetAllAsync();

            return View(blogs);
        }
    }
}
