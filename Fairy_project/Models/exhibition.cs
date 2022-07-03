using System;
namespace Fairy_project.Models;

public class Exhibition
{
    public Exhibition()
    {
    }
    public int exhibitId { get; set; }
    public string exhibitName { get; set; }
    public DateTime Date { get; set; }
    //Todo 時間格式
    public int exhibitStatus { get; set; }
    public string exhibit_P_img { get; set; }
    public string exhibit_T_img { get; set; }
    public string exhibit_Pre_img { get; set; }
}

public class Ticket
{
    public int orderId { get; set; }
    public int e_Id { get; set; }
    public int m_Id { get; set; }
    public int price { get; set; }
}

public class Member
{
    public Member()
    {

    }
    public int memberId { get; set; }
    public string? memberAc { get; set; }
    public string? memberName { get; set; }
    public string gender { get; set; }
    public string address { get; set; }
    public int phoneNumber { get; set; }
}

public class Booth
{
    public int boothId { get; set; }
    public int e_Id { get; set; }
    public int mf_Id { get; set; }
    public string? boothImg { get; set; }
    public string? mf_logo { get; set; }
    public string? mf_P_img { get; set; }
    public string? mf_Description { get; set; }
    public int checkStatus { get; set; }
    public int boothNumber { get; set; }
}

public class Manufacture
{
    public int manufactureName { get; set; }
    public string manufactureAcc { get; set; }
    public string mfPerson { get; set; }
    public string mfEmail { get; set; }
    public int mfPhoneNum { get; set; }
}

public class Permissions
{
    public string account { get; set; }
    public string password { get; set; }
    public int permissionsLv { get; set; }
}