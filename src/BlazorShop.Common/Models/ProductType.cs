﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShop.Common.Models;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [NotMapped]
    public bool Editing { get; set; }
    [NotMapped]
    public bool IsNew { get; set; }
}