using Services.Products;

namespace Services.Categories.Dto
{
    public record CategoryWithProductsDto(int id, string name, List<ProductDto> products);

}
