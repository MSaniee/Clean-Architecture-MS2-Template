namespace MS2Project.Application.Dtos.Products;

public class ProductDto
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }



    public string Name { get; set; }

    public ProductDto()
    {

    }

    public ProductDto(Guid id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }
}
