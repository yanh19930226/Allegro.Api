using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allegro.Api.Application.Services;
using Allegro.SDK;
using Allegro.SDK.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Allegro.Api.Controllers
{
    /// <summary>
    /// 商品类别管理
    /// </summary>
    [ApiController]
    [Route("Api/Category")]
    public class CategoryController : Controller
    {

        private readonly IAllegroCategoryService _categoryService;

        public CategoryController(IAllegroCategoryService categoryService)
        {

            _categoryService = categoryService;

        }
        /// <summary>
        /// Id获取商品类别
        /// </summary>
        /// <returns></returns>
        [Route("GetCategory")]
        [HttpGet]
        public async Task<AllegroResult<CategoryResponse>> GetContactDetails(string Id)
        {

            return await _categoryService.GetCategoryAsync(Id);
         
        }
    }
}
