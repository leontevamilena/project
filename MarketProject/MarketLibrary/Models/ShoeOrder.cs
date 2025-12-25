using System;
using System.Collections.Generic;

namespace MarketLibrary.Models;

public partial class ShoeOrder
{
    public int OrderId { get; set; }

    public int ShoeId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Shoe Shoe { get; set; } = null!;
}
