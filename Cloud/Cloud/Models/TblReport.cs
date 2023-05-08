using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblReport
{
    public int ReportId { get; set; }

    public int? EmplId { get; set; }

    public int? CompId { get; set; }

    public int? ReportApproved { get; set; }

    public DateTime? ReportDate { get; set; }

    public int? Status { get; set; }

    public TblReport(int reportId, int? emplId, int? compId, int? reportApproved, DateTime? reportDate, int? status)
    {
        ReportId = reportId;
        EmplId = emplId;
        CompId = compId;
        ReportApproved = reportApproved;
        ReportDate = reportDate;
        Status = status;
    }
}
