using Application.Products.Dto;
using AutoMapper;
using Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Command.CreateUpdateCommand
{
    public class ProductCommand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        //public string? ImageURL { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Product, ProductDto>();
            }
        }
    }
}
