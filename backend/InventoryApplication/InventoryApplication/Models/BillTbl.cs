using System;
using System.Collections.Generic;

namespace InventoryApplication.Models;

public partial class BillTbl
{
    public int BillId { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    public DateTime BillDate { get; set; }
}
