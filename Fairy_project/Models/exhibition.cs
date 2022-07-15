using System;
using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models;

public class Exhibition
{
    [Key]
    public int exhibitId { get; set; }
    public string? exhibitName { get; set; }
    public DateTime? datefrom { get; set; }
    public DateTime? dateto { get; set; }
    
    //Todo 時間格式
    public int exhibitStatus { get; set; }
    public string? exhibit_P_img { get; set; }
    public string? exhibit_T_img { get; set; }
    public string? exhibit_Pre_img { get; set; }
    public int? areaNum { get; set; }
    public int? ex_personTime { get; set; }
    public int? ex_totalImcome { get; set; }
}

