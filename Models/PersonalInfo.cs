using System;
using System.Collections.Generic;

namespace Identity_in_WebApi.Models;

public partial class PersonalInfo
{
    public int PersonId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Password { get; set; }

    public string? UserType { get; set; }

    public DateTime? LoginTime { get; set; }

    public byte[]? ProfileImage { get; set; }

}
