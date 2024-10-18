
namespace Services.Products.Request;
    public record CreateProductRequest(int Stock , string Name, decimal Price, int CategoryId);

