using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblEmployee
{
    public int EmplId { get; set; }

    public string? EmplFirstname { get; set; }

    public string? EmplLastname { get; set; }

    public int? CompId { get; set; }

    public string? EmplPassword { get; set; }

    public string? EmplEmail { get; set; }

    public int? Status { get; set; }

    public TblEmployee(int emplId, string? emplFirstname, string? emplLastname, int? compId, string? emplPassword, string? emplEmail, int? status)
    {
        EmplId = emplId;
        EmplFirstname = emplFirstname;
        EmplLastname = emplLastname;
        CompId = compId;
        EmplPassword = emplPassword;
        EmplEmail = emplEmail;
        Status = status;
    }


}
