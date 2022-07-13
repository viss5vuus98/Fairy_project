using System;
using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models;

public class Ticket
{
    [Key]
    public int orderNum { get; set; }
    public int? e_Id { get; set; }
    public int? m_Id { get; set; }
    public int price { get; set; }
    public int enterstate { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? entertime { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? ordertime { get; set; }
    public string? presonName { get; set; }
    public string? personNumber { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? payTime { get; set; }
}
