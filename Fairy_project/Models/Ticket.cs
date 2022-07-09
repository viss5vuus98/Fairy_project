using System;
using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models;

public class Ticket
{
    [Key]
    public int orderId { get; set; }
    public int e_Id { get; set; }
    public int m_Id { get; set; }
    public int price { get; set; }
}
