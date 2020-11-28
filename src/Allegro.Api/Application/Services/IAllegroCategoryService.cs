using Allegro.SDK;
using Allegro.SDK.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services
{
    public interface IAllegroCategoryService
    {
        Task<AllegroResult<CategoryResponse>> GetCategoryAsync(string categoryId);
    }
}
