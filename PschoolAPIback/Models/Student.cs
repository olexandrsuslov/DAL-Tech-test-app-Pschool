using System.ComponentModel.DataAnnotations;

namespace PschoolAPIback.Models;

public class Student
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneOne { get; set; }
    public int SiblingCount { get; set; }
    
    public Parent Parent { get; set; }
}