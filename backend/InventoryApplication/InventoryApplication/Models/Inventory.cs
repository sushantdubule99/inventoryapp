using System;
using System.Collections.Generic;

namespace InventoryApplication.Models;

public partial class Inventory
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int StockAvailable { get; set; }

    public int RecordStock { get; set; }

    public decimal Price { get; set; }
}
