using Allegro.SDK;
using Allegro.SDK.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services.Impl
{
    public class AllegroCategoryService : IAllegroCategoryService
    {
        private AllegroClient _client;
        public AllegroCategoryService(AllegroClient client)
        {
            _client = client;
        }
        public async Task<AllegroResult<CategoryResponse>> GetCategoryAsync(string categoryId)
        {
            GetCategoryRequest request = new GetCategoryRequest(categoryId);
            return await _client.GetAsync(request);
        }
    }
}
