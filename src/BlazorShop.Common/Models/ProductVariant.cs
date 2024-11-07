using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlazorShop.Common.Models;

public class ProductVariant
{
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; }
    [NotMapped]
    public bool Editing { get; set; }
    [NotMapped]
    public bool IsNew { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }
    public virtual ProductType? ProductType { get; set; }
}