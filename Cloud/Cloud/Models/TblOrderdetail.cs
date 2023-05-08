using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblOrderdetail
{
    public int EntryId { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    public int? CompId { get; set; }

    public int? Status { get; set; }

    public TblOrderdetail(int entryId, int? orderId, int? itemId, int? itemQuantity, int? compId, int? status)
    {
        EntryId = entryId;
        OrderId = orderId;
        ItemId = itemId;
        ItemQuantity = itemQuantity;
        CompId = compId;
        Status = status;
    }
}
