﻿using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Customer;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Models.DTO.Staff;
using EcommerceWeb.Models.DTO.Supplier;

namespace EcommerceWeb.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();

            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<AddPromotionDto, Promotion>().ReverseMap();
            CreateMap<UpdatePromotionDto, Promotion>().ReverseMap();
        }
    }
}
