using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using EgeApp.Backend.Business.Abstract;
using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.Dtos.CategoryDtos;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;
using EgeApp.Backend.Shared.Helpers;
using EgeApp.Backend.Shared.ResponseDtos;
using EgeApp.Backend.Models;
using Microsoft.EntityFrameworkCore;
using EgeApp.Backend.Data;
using System.Xml.Serialization;
using System.Linq.Expressions;

namespace EgeApp.Backend.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
          
            string url = CustomUrlHelper.GetUrl(categoryCreateDto.Name);
            Category category = _mapper.Map<Category>(categoryCreateDto);
            category.Url = url; 

            var createdCategory = await _categoryRepository.CreateAsync(category);
            if (createdCategory == null)
            {
                return ResponseDto<CategoryDto>.Fail("Kategori oluşturulamadı.", StatusCodes.Status400BadRequest);
            }

            CategoryDto categoryDto = _mapper.Map<CategoryDto>(createdCategory);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<CategoryDto>> CreateWithSubCategoriesAsync(CategoryCreateDto categoryCreateDto)
        {
            
            string url = CustomUrlHelper.GetUrl(categoryCreateDto.Name);
            Category category = _mapper.Map<Category>(categoryCreateDto);
            category.Url = url;

            var createdCategory = await _categoryRepository.CreateAsync(category);
            if (createdCategory == null)
            {
                return ResponseDto<CategoryDto>.Fail("Kategori oluşturulamadı.", StatusCodes.Status400BadRequest);
            }

            CategoryDto result = _mapper.Map<CategoryDto>(createdCategory);
            return ResponseDto<CategoryDto>.Success(result, StatusCodes.Status201Created);
        }
        public async Task<ResponseDto<NoContent>> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return ResponseDto<NoContent>.Fail($"{id} id'li kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            await _categoryRepository.DeleteAsync(category);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetActivesAsync(bool isActive = true)
        {
            var categoryList = await _categoryRepository.GetAllAsync(x => x.IsActive == isActive);
            string statusText = isActive ? "aktif" : "pasif";
            if (categoryList.Count == 0)
            {
                return ResponseDto<List<CategoryDto>>.Fail($"Hiç {statusText} kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDtoList = _mapper.Map<List<CategoryDto>>(categoryList);
            foreach (var categoryDto in categoryDtoList)
            {
                categoryDto.ProductCount = await _productRepository.GetCountAsync(x => x.CategoryId == categoryDto.Id);
            }

            return ResponseDto<List<CategoryDto>>.Success(categoryDtoList, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<int>> GetActivesCountAsync(bool isActive = true)
        {
            int count = await _categoryRepository.GetCountAsync(x => x.IsActive == isActive);
            string statusText = isActive ? "aktif" : "pasif";
            if (count == 0)
            {
                return ResponseDto<int>.Fail($"Hiç {statusText} kategori yok!", StatusCodes.Status404NotFound);
            }
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categoryList = await _categoryRepository.GetAllAsync();

            if (categoryList.Count == 0)
            {
                return ResponseDto<List<CategoryDto>>.Fail($"Hiç kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDtoList = _mapper.Map<List<CategoryDto>>(categoryList);
            return ResponseDto<List<CategoryDto>>.Success(categoryDtoList, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);

            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail($"{id} id'li kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<int>> GetCountAsync()
        {
            int count = await _categoryRepository.GetCountAsync();
            if (count == 0)
            {
                return ResponseDto<int>.Fail("Hiç kategori yok!", StatusCodes.Status404NotFound);
            }
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetHomeCategoriesAsync()
        {
            var homeCategories = await _categoryRepository.GetHomeAsync();
            var mappedCategories = _mapper.Map<List<CategoryDto>>(homeCategories);
            return ResponseDto<List<CategoryDto>>.Success(mappedCategories, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<(List<CategoryDto>, int)>> GetPagedCategoriesAsync(int pageIndex, int pageSize)
        {
            var (categories, totalCount) = await _categoryRepository.GetPagedAsync(pageIndex, pageSize);
            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
            return ResponseDto<(List<CategoryDto>, int)>.Success((mappedCategories, totalCount), StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetSortedCategoriesAsync<TKey>(Expression<Func<Category, TKey>> orderBy, bool isDescending = false)
        {
            var sortedCategories = await _categoryRepository.GetSortedAsync(orderBy, isDescending);
            var mappedCategories = _mapper.Map<List<CategoryDto>>(sortedCategories);
            return ResponseDto<List<CategoryDto>>.Success(mappedCategories, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<NoContent>> AddCategoriesAsync(IEnumerable<CategoryCreateDto> categories)
        {
            var entities = _mapper.Map<IEnumerable<Category>>(categories);
            await _categoryRepository.AddRangeAsync(entities);
            return ResponseDto<NoContent>.Success(StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<NoContent>> UpdateCategoriesAsync(IEnumerable<CategoryUpdateDto> categories)
        {
            var entities = _mapper.Map<IEnumerable<Category>>(categories);
            await _categoryRepository.UpdateRangeAsync(entities);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }


        public async Task<ResponseDto<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == categoryUpdateDto.Id);
            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Böyle bir kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedDate = DateTime.Now;
            category.Url = CustomUrlHelper.GetUrl(categoryUpdateDto.Name);
            await _categoryRepository.UpdateAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status200OK);
        }
    }
}