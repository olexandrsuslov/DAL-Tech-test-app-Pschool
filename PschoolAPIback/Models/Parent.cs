using System.ComponentModel.DataAnnotations;

namespace PschoolAPIback.Models;

public class Parent
{
    public int ParentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string Username { get; set; }
    public string Email { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string Address { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string PhoneOne { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string PhoneWork { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string PhoneHome { get; set; }
    public int SiblingCount { get; set; }
    public ICollection<Student> Students { get; set; }
}