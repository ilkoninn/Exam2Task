using App.Business.ViewModels.BlogVMs;
using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IQueryable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task CreateAsync(CreateBlogVM blog, string webRoot);
        Task UpdateAsync(UpdateBlogVM blog, string webRoot);
        Task DeleteAsync(int id);
    }
}
