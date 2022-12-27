using Pschool.Models.Dtos;
using PschoolAPIback.Models;

namespace PschoolAPIback.Extensions;

public static class DtoConversions
{
    public static IEnumerable<ParentDto> ConvertToDto(this IEnumerable<Parent> parents)
    {
        return (from parent in parents
            select new ParentDto
            {
                ParentId = parent.ParentId, 
                FirstName = parent.FirstName, 
                LastName = parent.LastName, 
                Username = parent.Username, 
                Email = parent.Email,
                Address = parent.Address,
                PhoneOne = parent.PhoneOne, 
                PhoneWork = parent.PhoneWork, 
                PhoneHome = parent.PhoneHome, 
                SiblingCount = parent.SiblingCount
            }).ToList();
    }

    public static ParentDto ConvertToDtoSingle(this Parent parent)
    {
        return new ParentDto
        {
            ParentId = parent.ParentId,
            FirstName = parent.FirstName,
            LastName = parent.LastName,
            Username = parent.Username,
            Email = parent.Email,
            Address = parent.Address,
            PhoneOne = parent.PhoneOne,
            PhoneWork = parent.PhoneWork,
            PhoneHome = parent.PhoneHome,
            SiblingCount = parent.SiblingCount
        };
    }

    public static Parent CovertToParentSingle(this ParentDto parent)
    {
        return new Parent
        {
            ParentId = parent.ParentId,
            FirstName = parent.FirstName,
            LastName = parent.LastName,
            Username = parent.Username,
            Email = parent.Email,
            Address = parent.Address,
            PhoneOne = parent.PhoneOne,
            PhoneWork = parent.PhoneWork,
            PhoneHome = parent.PhoneHome,
            SiblingCount = parent.SiblingCount
        };
    }
    
    public static IEnumerable<StudentDto> ConvertToDtoStudents(this IEnumerable<Student> students)
    {
        return (from student in students
            select new StudentDto
            {
                Id = student.Id, 
                ParentId = student.ParentId,
                FirstName = student.FirstName, 
                LastName = student.LastName, 
                Username = student.Username, 
                Email = student.Email,
                PhoneOne = student.PhoneOne,
                SiblingCount = student.SiblingCount
            }).ToList();
    }

    public static StudentDto ConvertToStudentDtoSingle(this Student student)
    {
        return new StudentDto
        {
            Id = student.Id, 
            ParentId = student.ParentId,
            FirstName = student.FirstName, 
            LastName = student.LastName, 
            Username = student.Username, 
            Email = student.Email,
            PhoneOne = student.PhoneOne,
            SiblingCount = student.SiblingCount
        };
    }

    public static Student CovertToStudentSingle(this StudentDto student)
    {
        return new Student
        {
            Id = student.Id, 
            ParentId = student.ParentId,
            FirstName = student.FirstName, 
            LastName = student.LastName, 
            Username = student.Username, 
            Email = student.Email,
            PhoneOne = student.PhoneOne,
            SiblingCount = student.SiblingCount
        };
    }
    
}