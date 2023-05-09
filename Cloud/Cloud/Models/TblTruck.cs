using System;
using System.Collections.Generic;

namespace Cloud.Models;

public partial class TblTruck
{
    public int TruckId { get; set; }

    public string? TruckModel { get; set; }

    public string? TruckLicense { get; set; }

    public int? EmplId { get; set; }

    public int? CompId { get; set; }

    public int? Status { get; set; }

    public TblTruck(int truckId, string? truckModel, string? truckLicense, int? emplId, int? compId, int? status)
    {
        TruckId = truckId;
        TruckModel = truckModel;
        TruckLicense = truckLicense;
        EmplId = emplId;
        CompId = compId;
        Status = status;
    }

    public TblTruck()
    {
    }
}
