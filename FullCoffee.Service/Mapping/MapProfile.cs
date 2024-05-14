using AutoMapper;
using FullCoffee.Core.DTOs;
using FullCoffee.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
          CreateMap<Product,ProductDto>().ReverseMap();
          CreateMap<Category,CategoryDto>().ReverseMap();
          CreateMap<ProductDetail,ProductDetailDto>().ReverseMap();
          CreateMap<Blog,BlogDto>().ReverseMap();
          CreateMap<User,UserDto>().ReverseMap();
          CreateMap<ProductUpdateDto,Product>();
        }
    }
}
