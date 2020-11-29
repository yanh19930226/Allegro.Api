using Allegro.SDK;
using Allegro.SDK.Models.Categories;
using Core.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services.Impl
{
    public class AllegroCategoryService : IAllegroCategoryService
    {
        private AllegroClient _client;
        private readonly IDatabase _redis;
        public AllegroCategoryService(AllegroClient client, RedisClient redisclient)
        {
            _client = client;
            _redis = redisclient.GetDatabase();
        }
        public async Task<AllegroResult<CategoryResponse>> GetCategoryAsync(string categoryId)
        {
            var token = _redis.StringGet("AllegroAppToken");
            GetCategoryRequest request = new GetCategoryRequest(token, categoryId);
            return await _client.GetAsync(request);
        }
    }
}
