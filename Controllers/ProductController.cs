using AutoMapper;
using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Image;
using EcommerceWeb.Models.DTO.Product;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly EcommerceWebDbContext dbContext;
        private readonly IImageRepository _imageRepository;

        public ProductController(
            IProductRepository productRepository, 
            IMapper mapper, 
            EcommerceWebDbContext dbContext, 
            IImageRepository imageRepository)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productDomainModel = await productRepository.GetAllAsync();

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
                ProductImages = product.ProductImages
                                .Select(pp => new ProductImageDto
                                {
                                    ProductImageId = pp.ProductImageId,
                                    FileName = pp.FileName,
                                    FileDescription = pp.FileDescription,
                                    FileExtension = pp.FileExtension,
                                    FileSizeInBytes = pp.FileSizeInBytes,
                                    FilePath = pp.FilePath,
                                })
                                .ToList(),
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
                ProductImages = product.ProductImages
                                .Select(pp => new ProductImageDto
                                {
                                    ProductImageId = pp.ProductImageId,
                                    FileName = pp.FileName,
                                    FileDescription = pp.FileDescription,
                                    FileExtension = pp.FileExtension,
                                    FileSizeInBytes = pp.FileSizeInBytes,
                                    FilePath = pp.FilePath,
                                })
                                .ToList(),
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

            var productPromotionList = new List<ProductPromotion>();
            if (addProductDto.PromotionIds != null)
            {
                foreach (var promotionId in addProductDto.PromotionIds)
                {
                    productPromotionList.Add(
                        new ProductPromotion
                        {
                            ProductId = productDomainModel.ProductId,
                            PromotionId = promotionId,
                            ProductPromotionId = Guid.NewGuid(),
                        }
                    );
                }

                productDomainModel.ProductPromotions = productPromotionList;
            }
            productDomainModel = await productRepository.CreateAsync(productDomainModel);

            var productImagesList = new List<ProductImage>();   
            if (addProductDto.ProductImageIds != null)
            {
                foreach (var productImageId in addProductDto.ProductImageIds)
                {
                    var existingProductImage = await _imageRepository.UpdateProductId(productImageId, productDomainModel.ProductId);
                    if (existingProductImage != null)
                    {
                        productImagesList.Add(existingProductImage);
                    }
                }
                productDomainModel.ProductImages = productImagesList;
                await dbContext.SaveChangesAsync();
            }

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
                ProductImages = productDomainModel.ProductImages
                                .Select(pp => new ProductImageDto
                                {
                                    ProductImageId = pp.ProductImageId,
                                    FileName = pp.FileName,
                                    FileDescription = pp.FileDescription,
                                    FileExtension = pp.FileExtension,
                                    FileSizeInBytes = pp.FileSizeInBytes,
                                    FilePath = pp.FilePath,
                                    ProductId = pp.ProductId,
                                })
                                .ToList(),
            };


            return CreatedAtAction(nameof(GetProduct), new { id = productDto.ProductId }, productDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductDto updateProductDto)
        {
            var productDomainModel = mapper.Map<Product>(updateProductDto);



            if (updateProductDto.ProductImageIds != null) 
            {
            
            productDomainModel = await productRepository.UpdateAsync(id, productDomainModel, updateProductDto.ProductImageIds);
            var producDto = mapper.Map<ProductDto>(productDomainModel);

            return Ok(producDto);
            }

            return BadRequest();

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var productDomain = await productRepository.DeleteAsync(id);

            if (productDomain != null)
            {

                var productDto = mapper.Map<ProductDto>(productDomain);

                return Ok(productDto);
            }

            return BadRequest();
        }
    }
}
