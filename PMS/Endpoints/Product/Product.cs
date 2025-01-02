using Application.Products;
using Application.Products.Command.CreateUpdateCommand;
using Application.Products.Command.DeleteCommand;

namespace PMS.Endpoints.Product
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", (ProductService productService) =>
            {
                var result = productService.GetProducts();

                return result;
            })
            .WithName("GetProducts")
            .WithTags("Products");

            app.MapGet("/api/products/{id:int}", (ProductService productService, int id) =>
            {
                var result = productService.GetProductById(id);

                return result;
            })
            .WithName("GetProductById")
            .WithTags("Products");

            app.MapPost("/api/products", async (ProductService productService, ProductCommand command) =>
            {
                var result = await productService.Add(command);

                return result;
            })
            .WithName("AddProduct")
            .WithTags("Products");

            app.MapPut("/api/products/{id:int}", async (ProductService productService, ProductCommand command, int id) =>
            {
                var result = await productService.Update(command, id);

                return result;
            })
            .WithName("UpdateProduct")
            .WithTags("Products");

            app.MapDelete("/api/products/{id:int}", async (ProductService productService,[AsParameters] DeleteProduct command) =>
            {
                var result = await productService.Delete(command);

                return result;
            })
            .WithName("DeleteProduct")
            .WithTags("Products");


            return app;
        }
    }
}
