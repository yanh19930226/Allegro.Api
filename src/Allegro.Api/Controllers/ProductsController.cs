﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allegro.Api.Abstractions.Dtos.Excels;
using Allegro.Api.Abstractions.Dtos.Requests.Products;
using Allegro.Api.Application.Services;
using Allegro.SDK;
using Allegro.SDK.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Util.Files;

namespace Allegro.Api.Controllers
{
    /// <summary>
    /// 产品
    /// </summary>
    [Route("Api/Products")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IAllegroProductService _productService;

        public ProductsController(IAllegroProductService productService)
        {
            _productService = productService;

        }
        /// <summary>
        /// 发布产品
        /// </summary>
        /// <returns></returns>
        [Route("ProposeProductAsync")]
        [HttpPost]
        public async Task<AllegroResult<ProposeProductResponse>> ProposeProductAsync([FromBody] ProposeProductRequestDto product)
        {
            return await _productService.ProposeProductAsync(product);
        }
        /// <summary>
        /// 上传Exccel发布产品
        /// </summary>
        /// <param name="excelfile"></param>
        /// <returns></returns>
        [Route("ExcelProposeProductAsync")]
        [HttpPost]
        public async Task<AllegroResult<ProposeProductResponse>> ExcelProposeProductAsync(IFormFile excelfile)
        {

            List<ShopAdvertisement> list = new ExcelHelper<ShopAdvertisement>().ImportFromExcel(excelfile);

            foreach (var item in list)
            {
                await _productService.ProposeProductAsync(new ProposeProductRequestDto() { });
            }

            return await _productService.ProposeProductAsync(new ProposeProductRequestDto() { });
        }

        /// <summary>
        /// 根据Id获取产品
        /// </summary>
        /// <param name="productId">产品Id</param>
        /// <returns></returns>
        [Route("GetProductByIdAsync")]
        [HttpGet]
        public async Task<AllegroResult<GetProductByIdResponse>> GetProductByIdAsync(string productId)
        {
            return await _productService.GetProductByIdAsync(productId);
        }
    }
}
