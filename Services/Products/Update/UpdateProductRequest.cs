namespace Services.Products.Update;

public record UpdateProductRequest(int Id, string Name, int Stock, decimal Price,int CategoryId);


