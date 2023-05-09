using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblOrder
{
    public int ReportId { get; set; }

    public int? CompId { get; set; }

    public int? OrderDelivered { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? Status { get; set; }

    public TblOrder(int reportId, int? compId, int? orderDelivered, DateTime? orderDate, int? status)
    {
        ReportId = reportId;
        CompId = compId;
        OrderDelivered = orderDelivered;
        OrderDate = orderDate;
        Status = status;
    }

    public TblOrder()
    {
    }
}
