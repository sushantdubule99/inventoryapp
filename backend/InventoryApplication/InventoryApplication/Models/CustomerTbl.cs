using System;
using System.Collections.Generic;

namespace InventoryApplication.Models;

public partial class CustomerTbl
{
    public int CustomerId { get; set; }

    public string CustomerEmail { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }
}
