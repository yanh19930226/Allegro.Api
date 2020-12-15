using Allegro.Api.Abstractions.Dtos.Requests.Products;
using Allegro.SDK;
using Allegro.SDK.Models.Products;
using Core.Extensions;
using Core.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services.Impl
{
    public class AllegroProductService : IAllegroProductService
    {
        //private readonly IDatabase _redis;

        private AllegroClient _client;

        public AllegroProductService(AllegroClient client/*, RedisClient redisclient*/)
        {
            _client = client;

            //_redis = redisclient.GetDatabase();
        }
        /// <summary>
        /// 发布产品
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<GetProductByIdResponse>> GetProductByIdAsync(string id)
        {
            //var request = new GetProductByIdRequest(new GetProductByIdRequestParameter { productId =id}, _redis.StringGet("AllegroAppToken"));

            //request.Request = RequestEnum.Api;

            //var res = await _client.GetAsync(request);

            //return res;

            return null;

        }
        /// <summary>
        ///发布产品
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<ProposeProductResponse>> ProposeProductAsync(ProposeProductRequestDto dto)
        {

            //var request = new ProposeProductRequest(new ProposeProductRequestParameter { name= dto.name, category = dto.category.MapTo<Category>() , images = dto.images.MapTo<List<Images>>() , parameters = dto.parameters.MapTo<List<Parameter>>() }, _redis.StringGet("AllegroAppToken"));

            //request.Request = RequestEnum.Api;

            //var res = await _client.PostAsync(request);

            //return res;

            return null;
        }
    }
}
