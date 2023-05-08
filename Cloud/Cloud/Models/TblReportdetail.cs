using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblReportdetail
{
    public int EntryId { get; set; }

    public int? ReportId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    public int? CompId { get; set; }

    public int? Status { get; set; }

    public TblReportdetail(int entryId, int? reportId, int? itemId, int? itemQuantity, int? compId, int? status)
    {
        EntryId = entryId;
        ReportId = reportId;
        ItemId = itemId;
        ItemQuantity = itemQuantity;
        CompId = compId;
        Status = status;
    }
}
