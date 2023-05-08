using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblTruckItem
{
    public int EntryId { get; set; }

    public int? TruckId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    public int? Status { get; set; }

    public TblTruckItem(int entryId, int? truckId, int? itemId, int? itemQuantity, int? status)
    {
        EntryId = entryId;
        TruckId = truckId;
        ItemId = itemId;
        ItemQuantity = itemQuantity;
        Status = status;
    }
}
