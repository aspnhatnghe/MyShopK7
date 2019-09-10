using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

namespace UI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CartItem>()
                .ForMember(dest => dest.MaHh, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.TenHh, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DonGia, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.GiamGia, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.Hinh, opt => opt.MapFrom(src => src.MainImage));
        }
    }
}
