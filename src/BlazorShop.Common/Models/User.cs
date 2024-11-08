﻿namespace BlazorShop.Common.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string Role { get; set; } = "Customer";

    public virtual Address Address { get; set; } = null!;
}