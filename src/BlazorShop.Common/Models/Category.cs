﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShop.Common.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; }
    [NotMapped]
    public bool Editing { get; set; }
    [NotMapped]
    public bool IsNew { get; set; }
}