using System;
using System.Collections.Generic;

namespace MarketLibrary.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string? Status { get; set; }

    public int UserId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int ReceiveCode { get; set; }

    public virtual ICollection<ShoeOrder> ShoeOrders { get; set; } = new List<ShoeOrder>();

    public virtual User User { get; set; } = null!;
}
