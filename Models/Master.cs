// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace VinylRelease.Models;

public partial class Master
{
    public long MasterId { get; set; }

    public string MasterName { get; set; }

    public short? MasterYear { get; set; }

    public virtual ICollection<Artist> Artists { get; set;  } = new List<Artist>();
}