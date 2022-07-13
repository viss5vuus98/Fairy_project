using System;
using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models;

public class Member
{
    [Key]
    public int memberId { get; set; }
    [MaxLength(250)]
    public string? memberAc { get; set; }
    [MaxLength(50)]
    public string? memberName { get; set; }
    public string? gender { get; set; }
    public string? address { get; set; }
    public string? phoneNumber { get; set; }

}
