using System;
using System.ComponentModel.DataAnnotations;
namespace Fairy_project.Models;

public class Area
{
    [Key]
    public int areaNumber { get; set; }
    public string? areaSize { get; set; }
    public string? limitBooth { get; set; }
    public int? limitPeople { get; set; }
}

