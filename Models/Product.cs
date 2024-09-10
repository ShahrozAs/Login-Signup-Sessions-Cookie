using System;
using System.Collections.Generic;

namespace Identity_in_WebApi.Models;

public partial class Product
{
    public int PId { get; set; }

    public int? PersonId { get; set; }

    public string? PName { get; set; }

    public int? PStoke { get; set; }

    public double? Price { get; set; }

    public DateTime? AddedDate { get; set; }

}
