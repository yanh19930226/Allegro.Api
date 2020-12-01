using Allegro.Api.Abstractions.Dtos.Commons;
using Allegro.SDK.Models.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allegro.Api.Application.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<ImagesDto, Images>();
            CreateMap<ParameterDto, Parameter>();
        }
    }
}
