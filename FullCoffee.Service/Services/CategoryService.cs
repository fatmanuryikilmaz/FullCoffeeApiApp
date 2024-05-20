using AutoMapper;
using FullCoffee.Core.DTOs;
using FullCoffee.Core.Models;
using FullCoffee.Core.Repositories;
using FullCoffee.Core.Services;
using FullCoffee.Core.UnitOfWorks;
using FullCoffee.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FikaCoffeeShop.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> repository, IMapper mapper, ICategoryRepository categoryRepository) : base(unitOfWork, repository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);
            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
        }
    }
}
