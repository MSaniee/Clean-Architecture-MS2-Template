namespace MS2Project.Application.Dtos.Orders;

public class OrderDto
{
    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public string Currency { get; set; }

    public bool IsRemoved { get; set; }
}

