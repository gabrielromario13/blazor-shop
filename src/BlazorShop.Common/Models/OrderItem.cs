using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShop.Common.Models;

public class OrderItem
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    public virtual ProductType ProductType { get; set; } = null!;
}