﻿using Allegro.Api.Abstractions.Dtos.Requests.Products;
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
        private readonly IDatabase _redis;

        private AllegroClient _client;

        public AllegroProductService(AllegroClient client, RedisClient redisclient)
        {
            _client = client;

            _redis = redisclient.GetDatabase();
        }
        /// <summary>
        /// 发布产品
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<GetProductByIdResponse>> GetProductByIdAsync(string id)
        {
            var request = new GetProductByIdRequest(id, _redis.StringGet("AllegroAppToken"));

            request.Request = RequestEnum.Api;

            var res = await _client.GetAsync<GetProductByIdResponse>(request);
        
            return res;

        }
        /// <summary>
        ///发布产品
        /// </summary>
        /// <returns></returns>
        public async Task<AllegroResult<ProposeProductResponse>> ProposeProductAsync(ProposeProductRequestDto dto)
        {
            var request = new ProposeProductRequest(dto.name,dto.category.MapTo<Category>(),dto.images.MapTo<List<Images>>(), dto.parameters.MapTo<List<Parameter>>(), _redis.StringGet("AllegroAppToken"));

            request.Request = RequestEnum.Api;

            var res = await _client.PostAsync<ProposeProductResponse>(request);

            return res;
        }
    }
}
