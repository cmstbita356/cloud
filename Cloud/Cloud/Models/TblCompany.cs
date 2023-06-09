﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cloud.Models;

public partial class TblCompany
{
    [Key]
    public int CompId { get; set; }
    [Required]
    public string? CompName { get; set; }
    [Required]
    public string? CompEmail { get; set; }
    [Required]
    public string? CompPhone { get; set; }
    [Required]
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

    public TblCompany()
    {
    }
}
