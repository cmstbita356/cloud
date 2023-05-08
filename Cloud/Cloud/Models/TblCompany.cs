using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblCompany
{
    public int CompId { get; set; }

    public string? CompName { get; set; }

    public string? CompEmail { get; set; }

    public string? CompPhone { get; set; }

    public string? CompPassword { get; set; }

    public int? Status { get; set; }

    public TblCompany(int compId, string? compName, string? compEmail, string? compPhone, string? compPassword, int? status)
    {
        CompId = compId;
        CompName = compName;
        CompEmail = compEmail;
        CompPhone = compPhone;
        CompPassword = compPassword;
        Status = status;
    }
}
