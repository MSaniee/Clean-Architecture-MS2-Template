using MS2Project.Application.Dtos.Products;

namespace MS2Project.Application.Dtos.Orders;

public class CustomerOrderDto
{
    public List<ProductDto> Products { get; set; }

    public string Currency { get; set; }
}
