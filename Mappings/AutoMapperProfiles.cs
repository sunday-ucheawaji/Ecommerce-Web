using AutoMapper;
using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Customer;
using EcommerceWeb.Models.DTO.Image;
using EcommerceWeb.Models.DTO.Product;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Models.DTO.Staff;
using EcommerceWeb.Models.DTO.Supplier;
using EcommerceWeb.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        private readonly IMapper mapper;
        private readonly EcommerceWebDbContext dbContext;

        public AutoMapperProfiles()
        {

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();

            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<AddPromotionDto, Promotion>().ReverseMap();
            CreateMap<UpdatePromotionDto, Promotion>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<Promotion, PromotionDto>();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<UpdateImageUploadDto,  ProductImage>().ReverseMap();

            CreateMap<Product, ProductWithPromotionsDto>()
                .ForMember(dest => dest.Promotions, opt => opt.MapFrom(src => src.ProductPromotions.Select(pp => pp.Promotion).ToList()));


            CreateMap<AddProductDto, Product>().ReverseMap();
            //CreateMap<AddProductDto, Product>()
            //    .AfterMap((src, dest) =>
            //    {
            //        if (src.PromotionIds != null)
            //        {
            //            dest.ProductPromotions = new List<ProductPromotion>();
            //            foreach (var promotionId in src.PromotionIds)
            //            {
            //                var promotion = dbContext.Promotions.FirstOrDefault(x=> x.PromotionId == promotionId);
            //                if (promotion != null)
            //                {
            //                    dest.ProductPromotions.Add(new ProductPromotion
            //                    {
            //                        PromotionId = promotionId,
            //                        Promotion = promotion,
            //                        Product = dest
            //                    });
            //                }
            //            }
            //        }
            //    });
        }

       

     

    }
}
