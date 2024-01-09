using AutoMapper;
using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Product;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly EcommerceWebDbContext dbContext;

        public ProductController(IProductRepository productRepository, IMapper mapper, EcommerceWebDbContext dbContext)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productDomainModel = await productRepository.GetAllAsync();

            //var productDto = mapper.Map<List<ProductWithPromotionsDto>>(productDomainModel);

            var productDto = productDomainModel.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsFeatured = product.IsFeatured,
                IsArchived = product.IsArchived,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Promotions = product.ProductPromotions
                            .Select(pp => new PromotionDto
                            {
                                PromotionId = pp.Promotion.PromotionId,
                                Title = pp.Promotion.Title,
                                Description = pp.Promotion.Description,
                                StartDate = pp.Promotion.StartDate,
                                EndDate = pp.Promotion.EndDate,
                                DiscountPercentage = pp.Promotion.DiscountPercentage
                            })
                            .ToList(),
                ProductImages = product.ProductImages,
                OrderDetails = product.OrderDetails,
                Categories = product.Categories
            }).ToList();

            return Ok(productDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsFeatured = product.IsFeatured,
                IsArchived = product.IsArchived,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Promotions = product.ProductPromotions
                            .Select(pp => new PromotionDto
                            {
                                PromotionId = pp.Promotion.PromotionId,
                                Title = pp.Promotion.Title,
                                Description = pp.Promotion.Description,
                                StartDate = pp.Promotion.StartDate,
                                EndDate = pp.Promotion.EndDate,
                                DiscountPercentage = pp.Promotion.DiscountPercentage
                            })
                            .ToList(),
                ProductImages = product.ProductImages,
                OrderDetails = product.OrderDetails,
                Categories = product.Categories
            };


            return Ok(productDto);
        }

        [HttpPost]
        [Route("")]

        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto addProductDto)
        {
            var productDomainModel = mapper.Map<Product>(addProductDto);

            var promotionList = new List<ProductPromotion>();
            if (addProductDto.PromotionIds != null)
            {
                foreach (var promotionId in addProductDto.PromotionIds)
                {
                    promotionList.Add(
                        new ProductPromotion
                        {
                            ProductId = productDomainModel.ProductId,
                            PromotionId = promotionId,
                            ProductPromotionId = Guid.NewGuid(),
                        }
                    );
                }

                productDomainModel.ProductPromotions = promotionList;
            }
            productDomainModel = await productRepository.CreateAsync(productDomainModel);

            //var productDto = mapper.Map<ProductDto>(productDomainModel);
            productDomainModel = await productRepository.GetByIdAsync(productDomainModel.ProductId);
            var productDto = new ProductDto
            {
                ProductId = productDomainModel.ProductId,
                Name = productDomainModel.Name,
                Description = productDomainModel.Description,
                StockQuantity = productDomainModel.StockQuantity,
                IsArchived = productDomainModel.IsArchived,
                IsFeatured = productDomainModel.IsFeatured,
                CreatedAt = productDomainModel.CreatedAt,
                UpdatedAt = productDomainModel.UpdatedAt,
                Promotions = productDomainModel.ProductPromotions
                            .Select(pp => new PromotionDto
                            {
                                PromotionId = pp.Promotion.PromotionId,
                                Title = pp.Promotion.Title,
                                Description = pp.Promotion.Description,
                                StartDate = pp.Promotion.StartDate,
                                EndDate = pp.Promotion.EndDate,
                                DiscountPercentage = pp.Promotion.DiscountPercentage
                            })
                            .ToList(),
            };


            return CreatedAtAction(nameof(GetProduct), new { id = productDto.ProductId }, productDto);
        }
    }
}
