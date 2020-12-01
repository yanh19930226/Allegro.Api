using Allegro.Api.Abstractions.Dtos.Requests.Products;
using Allegro.SDK;
using Allegro.SDK.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Services
{
    public interface IAllegroProductService
    {
        Task<AllegroResult<ProposeProductResponse>> ProposeProductAsync(ProposeProductRequestDto product);

        Task<AllegroResult<GetProductByIdResponse>> GetProductByIdAsync(string id);
    }
}
