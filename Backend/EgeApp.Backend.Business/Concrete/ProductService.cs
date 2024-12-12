using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EgeApp.Backend.Business.Abstract;
using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.Dtos.CategoryDtos;
using EgeApp.Backend.Shared.Dtos.ProductDtos;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;
using EgeApp.Backend.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgeApp.Backend.Models;
using EgeApp.Backend.Shared.ResponseDtos;
using EgeApp.Backend.Data;

namespace EgeApp.Backend.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            Product product = _mapper.Map<Product>(productCreateDto);
            product.Url = CustomUrlHelper.GetUrl(productCreateDto.Name);
            product.IsActive = product.IsHome ? true : product.IsActive;
            var createdProduct = await _productRepository.CreateAsync(product);

            if (createdProduct == null)
            {
                return ResponseDto<ProductDto>.Fail("Bir hata oluştu", StatusCodes.Status400BadRequest);
            }

            ProductDto createdProductDto = _mapper.Map<ProductDto>(createdProduct);
            createdProductDto.Category = _mapper.Map<CategoryDto>(
                await _categoryRepository.GetAsync(x => x.Id == createdProductDto.CategoryId));
            return ResponseDto<ProductDto>.Success(createdProductDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(int id)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == id);
            if (product == null)
            {
                return ResponseDto<NoContent>.Fail($"{id} id'li ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            await _productRepository.DeleteAsync(product);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetActivesAsync(bool isActive = true)
        {
            List<Product> productList = await _productRepository.GetAllAsync(x => x.IsActive == isActive, x => x.Include(y => y.Category));
            if (!productList.Any())
            {
                string statusText = isActive ? "aktif" : "pasif";
                return ResponseDto<List<ProductDto>>.Fail($"Hiç {statusText} ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            List<ProductDto> productDtoList = _mapper.Map<List<ProductDto>>(productList);
            return ResponseDto<List<ProductDto>>.Success(productDtoList, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<int>> GetActivesCountAsync(bool isActive = true)
        {
            int count = await _productRepository.GetCountAsync(x => x.IsActive == isActive);
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            List<Product> productList = await _productRepository.GetAllAsync(null, x => x.Include(y => y.Category));
            if (!productList.Any())
            {
                return ResponseDto<List<ProductDto>>.Fail($"Hiç ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            List<ProductDto> productDtoList = _mapper.Map<List<ProductDto>>(productList);
            return ResponseDto<List<ProductDto>>.Success(productDtoList, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllByCategoryIdAsync(int categoryId)
        {
            List<Product> productList = await _productRepository.GetAllAsync(
                x => x.IsActive && x.CategoryId == categoryId,
                x => x.Include(y => y.Category));

            var category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (category == null)
            {
                return ResponseDto<List<ProductDto>>.Fail("Böyle bir kategori yok!", StatusCodes.Status404NotFound);
            }

            if (!productList.Any())
            {
                return ResponseDto<List<ProductDto>>.Fail($"{category.Name} kategorisinde hiç ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            List<ProductDto> productDtoList = _mapper.Map<List<ProductDto>>(productList);
            return ResponseDto<List<ProductDto>>.Success(productDtoList, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetBestSellersAsync(int topCount = 10)
        {
            List<Product> bestSellers = await _productRepository.GetAllAsync(
                x => x.IsActive,
                query => query.OrderByDescending(p => p.SalesCount).Take(topCount).Include(p => p.Category));

            if (!bestSellers.Any())
            {
                return ResponseDto<List<ProductDto>>.Fail("Hiç satış yapılmamış ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            List<ProductDto> bestSellerDtos = _mapper.Map<List<ProductDto>>(bestSellers);
            return ResponseDto<List<ProductDto>>.Success(bestSellerDtos, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetDiscountedProductsAsync()
        {
            List<Product> discountedProducts = await _productRepository.GetAllAsync(
                x => x.IsDiscounted && x.IsActive,
                x => x.Include(y => y.Category));

            if (!discountedProducts.Any())
            {
                return ResponseDto<List<ProductDto>>.Fail("İndirimde ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            List<ProductDto> discountedProductDtos = _mapper.Map<List<ProductDto>>(discountedProducts);
            return ResponseDto<List<ProductDto>>.Success(discountedProductDtos, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(int id)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == id, x => x.Include(y => y.Category));
            if (product == null)
            {
                return ResponseDto<ProductDto>.Fail($"{id} id'li ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            ProductDto productDto = _mapper.Map<ProductDto>(product);
            return ResponseDto<ProductDto>.Success(productDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<int>> GetCountAsync()
        {
            int count = await _productRepository.GetCountAsync();
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetHomeAsync(bool isHome = true)
        {
            List<Product> productList = await _productRepository.GetAllAsync(x => x.IsHome == isHome, x => x.Include(y => y.Category));
            if (!productList.Any())
            {
                return ResponseDto<List<ProductDto>>.Fail("Hiç ana sayfa ürünü bulunamadı!", StatusCodes.Status404NotFound);
            }

            List<ProductDto> productDtoList = _mapper.Map<List<ProductDto>>(productList);
            return ResponseDto<List<ProductDto>>.Success(productDtoList, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<int>> GetHomeCountAsync(bool isHome = true)
        {
            int count = await _productRepository.GetCountAsync(x => x.IsHome == isHome);
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == productUpdateDto.Id);
            if (product == null)
            {
                return ResponseDto<ProductDto>.Fail($"{productUpdateDto.Id} id'li ürün bulunamadı", StatusCodes.Status404NotFound);
            }

            var createdDate = product.CreatedDate;
            product = _mapper.Map<Product>(productUpdateDto);
            product.CreatedDate = createdDate;
            product.ModifiedDate = DateTime.Now;
            product.Url = CustomUrlHelper.GetUrl(productUpdateDto.Name);
            product.IsActive = product.IsHome ? true : product.IsActive;

            await _productRepository.UpdateAsync(product);

            ProductDto productDto = _mapper.Map<ProductDto>(product);
            productDto.Category = _mapper.Map<CategoryDto>(await _categoryRepository.GetAsync(x => x.Id == productDto.CategoryId));

            return ResponseDto<ProductDto>.Success(productDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<NoContent>> UpdateIsActiveAsync(int id)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == id);
            if (product == null)
            {
                return ResponseDto<NoContent>.Fail($"{id} id'li bir ürün bulunamadı", StatusCodes.Status404NotFound);
            }

            product.IsActive = !product.IsActive;
            product.IsHome = !product.IsActive ? false : product.IsHome;
            await _productRepository.UpdateAsync(product);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<NoContent>> UpdateIsHomeAsync(int id)
        {
            Product product = await _productRepository.GetAsync(x => x.Id == id);
            if (product == null)
            {
                return ResponseDto<NoContent>.Fail($"{id} id'li bir ürün bulunamadı", StatusCodes.Status404NotFound);
            }

            product.IsHome = !product.IsHome;
            product.IsActive = product.IsHome ? true : product.IsActive;
            await _productRepository.UpdateAsync(product);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        Task<ResponseDto<Microsoft.AspNetCore.Http.HttpResults.NoContent>> IProductService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<Microsoft.AspNetCore.Http.HttpResults.NoContent>> IProductService.UpdateIsActiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<Microsoft.AspNetCore.Http.HttpResults.NoContent>> IProductService.UpdateIsHomeAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
