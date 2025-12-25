using System;
using System.Collections.Generic;

namespace MarketLibrary.Models;

public partial class Shoe
{
    public int ShoeId { get; set; }

    public int VendorId { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Article { get; set; } = null!;

    public int Price { get; set; }

    public int Discount { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public int? Size { get; set; }

    public string? Color { get; set; }

    public string Gender { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ShoeOrder> ShoeOrders { get; set; } = new List<ShoeOrder>();

    public virtual Vendor Vendor { get; set; } = null!;
}
