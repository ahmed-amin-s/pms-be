using Application.Common.Models;
using Application.Interfaces;
using Application.Products.Command.CreateUpdateCommand;
using Application.Products.Command.DeleteCommand;
using Application.Products.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products
{
    public class ProductService
    {
        private readonly IPmsRepository<Product> _repository;
        private readonly IValidator<ProductCommand> _validator;
        private readonly IMapper _mapper;
        public ProductService(IPmsRepository<Product> repository, IMapper mapper, IValidator<ProductCommand> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }


        public Result<List<ProductDto>> GetProducts()
        {
            try
            {
                var products = _repository.GetAll()
                                          .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                          .ToList();

                return new Result<List<ProductDto>>
                {
                    Succeeded = true,
                    Message = "Products Loaded Successfully",
                    Data = products
                };
            }
            catch (Exception _Exception)
            {
                return Result<List<ProductDto>>.Failed(_Exception.Message);
            }
        }

        public Result<ProductDto> GetProductById(int id)
        {
            try
            {
                var product = _repository.GetAll().Where(i => i.Id == id)
                                      .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefault();

                if (product is null)
                    return Result<ProductDto>.NotFound();

                return new Result<ProductDto>
                {
                    Succeeded = true,
                    Message = "Product is Loaded Successfully",
                    Data = product
                };
            }
            catch (Exception _Exception)
            {
                return Result<ProductDto>.Failed(_Exception.Message);
            }
        }

        public async Task<Result<int>> Add(ProductCommand command)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return Result<int>.Failed(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }


                var entity = _mapper.Map<Product>(command);

                if (entity is null)
                    return Result<int>.NotFound();
                
                entity.CreatedDate = DateTime.Now;

                var count = _repository.Add(entity);

                return new Result<int>
                {
                    Succeeded = true,
                    Message = "Record Added Successfully.",
                    Data = await Task.FromResult(count)
                };
            }
            catch (Exception _Exception)
            {
                return Result<int>.Failed(_Exception.Message);
            }
        }

        public async Task<Result<int>> Update(ProductCommand command, int id)
        {
            try
            {
                if (command.Id != id)
                {
                    return Result<int>.Failed("BadRequest");
                }

                var validationResult = await _validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return Result<int>.Failed(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }


                var entity = _repository.GetById(command.Id);

                if (entity is null)
                    return Result<int>.NotFound();


                _mapper.Map(command, entity);
                var result = _repository.Update(entity);
                return Result<int>.Succeed(entity.Id);
            }
            catch
            {
                return Result<int>.Failed("Updating failed!");
            }
        }

        public async Task<Result<int>> Delete(DeleteProduct command)
        {
            try
            {
                var entity = _repository.GetById(command.Id);

                if (entity is null)
                    return Result<int>.NotFound();

                var count = _repository.Delete(entity.Id);
              
                return new Result<int>
                {
                    Succeeded = true,
                    Message = "Record Deleted Successfully.",
                    Data = await Task.FromResult(count)
                };
            }
            catch (Exception _Exception)
            {
                return Result<int>.Failed(_Exception.Message);
            }
        }
    }
}
