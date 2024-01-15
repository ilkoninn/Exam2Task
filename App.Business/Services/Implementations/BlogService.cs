using App.Business.Exceptions.BlogExceptions;
using App.Business.Exceptions.Common;
using App.Business.Helpers;
using App.Business.Services.Interfaces;
using App.Business.ViewModels.BlogVMs;
using App.Core.Entities;
using App.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repBlog;
        public BlogService(IBlogRepository repBlog)
        {
            _repBlog = repBlog;
        }

        public async Task<IQueryable<Blog>> GetAllAsync()
        {
            return await _repBlog.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _repBlog.GetByIdAsync(id);
        }

        public async Task CreateAsync(CreateBlogVM blog, string webRoot)
        {
            if (blog.Title is null)
            {
                throw new ObjectRequiredException("Object is required!", nameof(blog.Title));
            }

            if (!blog.File.CheckLength(2097350))
            {
                throw new BlogImageException("Image size must be over than 2MB!", nameof(blog.File));
            }
            if (!blog.File.CheckType("image/"))
            {
                throw new BlogImageException("File must be image type!", nameof(blog.File));
            }

            if (blog.Description is null)
            {
                throw new ObjectRequiredException("Object is required!", nameof(blog.Description));
            }

            Blog newBlog = new()
            {
                Title = blog.Title,
                Description = blog.Description,
                CoverImgUrl = blog.File.Upload(webRoot, @"\Upload\BlogImages\"),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _repBlog.CreateAsync(newBlog);
            await _repBlog.SaveChangeAsync();
        }
        public async Task UpdateAsync(UpdateBlogVM blog, string webRoot)
        {
            if (blog.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(blog.Id));

            Blog oldBlog = await _repBlog.GetByIdAsync(blog.Id);

            if(oldBlog is null) throw new BlogNotFoundException("There is no blog in data!", nameof(oldBlog));
                
            if(blog.Title is null)
            {
                throw new ObjectRequiredException("Object is required!", nameof(blog.Title));
            }
            if (blog.Description is null)
            {
                throw new ObjectRequiredException("Object is required!", nameof(blog.Description));
            }

            oldBlog.Title = blog.Title;
            oldBlog.Description = blog.Description;
            oldBlog.UpdatedDate = DateTime.Now;
            
            if(blog.File is not null)
            {
                if (!blog.File.CheckLength(2097350))
                {
                    throw new BlogImageException("Image size must be over than 2MB!", nameof(blog.File));
                }
                if (!blog.File.CheckType("image/"))
                {
                    throw new BlogImageException("File must be image type!", nameof(blog.File));
                }

                FileManager.Delete(oldBlog.CoverImgUrl, webRoot, @"\Upload\BlogImages\");
                oldBlog.CoverImgUrl = blog.File.Upload(webRoot, @"\Upload\BlogImages\");
            }

            await _repBlog.UpdateAsync(oldBlog);
            await _repBlog.SaveChangeAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            await _repBlog.DeleteAsync(id);
            await _repBlog.SaveChangeAsync();
        }

        
    }
}
