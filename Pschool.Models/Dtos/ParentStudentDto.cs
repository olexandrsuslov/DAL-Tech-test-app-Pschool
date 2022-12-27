using System.ComponentModel.DataAnnotations;

namespace Pschool.Models.Dtos;

public class ParentDto
{
    public int ParentId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string PhoneOne { get; set; }
    [Required]
    public string PhoneWork { get; set; }
    [Required]
    public string PhoneHome { get; set; }
    [Required]
    public int SiblingCount { get; set; }
}

public class StudentDto
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneOne { get; set; }
    [Required]
    public int SiblingCount { get; set; }
}

