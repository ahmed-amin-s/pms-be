using Application.Products.Command.CreateUpdateCommand;
using AutoMapper;
using Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        //public string? ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public class ProductMappingProfile : Profile
        {
            public ProductMappingProfile()
            {
                CreateMap<Product, ProductDto>().ReverseMap();
                CreateMap<Product, ProductCommand>().ReverseMap();
            }
        }
    }
}
