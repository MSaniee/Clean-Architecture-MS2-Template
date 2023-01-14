using MS2Project.Application.Dtos.Products;

namespace MS2Project.Application.Dtos.Orders;

public class OrderDetailsDto
{
    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public string Currency { get; set; }

    public bool IsRemoved { get; set; }

    public List<ProductDto> Products { get; set; }
}

