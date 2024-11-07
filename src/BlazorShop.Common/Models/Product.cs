using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShop.Common.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public bool Featured { get; set; }
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; }
    [NotMapped]
    public bool Editing { get; set; }
    [NotMapped]
    public bool IsNew { get; set; }

    public virtual Category? Category { get; set; }
    public virtual List<Image> Images { get; set; } = [];
    public virtual List<ProductVariant> Variants { get; set; } = [];
}