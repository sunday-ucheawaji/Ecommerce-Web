using AutoMapper;
using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Address;
using EcommerceWeb.Models.DTO.BillBoard;
using EcommerceWeb.Models.DTO.Cart;
using EcommerceWeb.Models.DTO.CartItem;
using EcommerceWeb.Models.DTO.Category;
using EcommerceWeb.Models.DTO.Customer;
using EcommerceWeb.Models.DTO.Image;
using EcommerceWeb.Models.DTO.Order;
using EcommerceWeb.Models.DTO.OrderDetailFolder;
using EcommerceWeb.Models.DTO.Product;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Models.DTO.Review;
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
            CreateMap<Promotion, AddPromotionDto>().ReverseMap();
            CreateMap<Promotion, UpdatePromotionDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, AddProductDto>().ReverseMap();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<ProductImage, UpdateImageUploadDto>().ReverseMap();

            CreateMap<Promotion, PromotionDto>();


            CreateMap<Product, ProductWithPromotionsDto>()
                .ForMember(dest => dest.Promotions, opt => opt.MapFrom(src => src.ProductPromotions.Select(pp => pp.Promotion).ToList()));


            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<Cart, UpdateCartDTo>().ReverseMap();

            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<CartItem, AddCartItemDto>().ReverseMap();

            //CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => (OrderDto.PaymentStatusEnum)src.PaymentStatus));


            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, AddOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailDto>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddAddressDto>().ReverseMap();
            CreateMap<Address, UpdateAddressDto>().ReverseMap();

            CreateMap<BillBoard, BillBoardDto>().ReverseMap();
            CreateMap<BillBoard, AddBillBoardDto>().ReverseMap();
            CreateMap<BillBoard, UpdateBillBoardDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, AddCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, AddReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();

        }


    }
}
