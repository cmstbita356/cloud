using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblItem
{
    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public int? ItemInstock { get; set; }

    public int? CompId { get; set; }

    public int? Status { get; set; }

    public TblItem(int itemId, string? itemName, int? itemInstock, int? compId, int? status)
    {
        ItemId = itemId;
        ItemName = itemName;
        ItemInstock = itemInstock;
        CompId = compId;
        Status = status;
    }
}
