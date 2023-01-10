using System.Reflection;
using System.Text;
using PschoolAPIback.Models;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
namespace PschoolAPIback.Extensions;

public static class ParentStudentExtensions
{
    public static IQueryable<Parent> Search(this IQueryable<Parent> parents, string searchTearm)
    {
        if (string.IsNullOrWhiteSpace(searchTearm))
            return parents;

        var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

        return parents.Where(p => p.FirstName.ToLower().Contains(lowerCaseSearchTerm) || p.LastName.ToLower().Contains(lowerCaseSearchTerm));
    }
    
    public static IQueryable<Student> Search(this IQueryable<Student> students, string searchTearm)
    {
        if (string.IsNullOrWhiteSpace(searchTearm))
            return students;

        var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

        return students.Where(p => p.FirstName.ToLower().Contains(lowerCaseSearchTerm) || p.LastName.ToLower().Contains(lowerCaseSearchTerm));
    }
    
    //sort
    public static IQueryable<Student> Sort(this IQueryable<Student> students, string orderByQueryString) 
    { 
        if (string.IsNullOrWhiteSpace(orderByQueryString)) 
            return students.OrderBy(e => e.FirstName); 
            
        var orderParams = orderByQueryString.Trim().Split(','); 
        var propertyInfos = typeof(Student).GetProperties(BindingFlags.Public | BindingFlags.Instance); 
        var orderQueryBuilder = new StringBuilder(); 
            
        foreach (var param in orderParams) 
        { 
            if (string.IsNullOrWhiteSpace(param)) 
                continue; 
                
            var propertyFromQueryName = param.Split(" ")[0]; 
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase)); 
                
            if (objectProperty == null) 
                continue; 
                
            var direction = param.EndsWith(" desc") ? "descending" : "ascending"; 
            orderQueryBuilder.Append($"{objectProperty.Name} {direction}, "); 
        } 
            
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); 
        if (string.IsNullOrWhiteSpace(orderQuery)) 
            return students.OrderBy(e => e.FirstName); 
            
        return students.OrderBy(orderQuery); 
    }
    
    public static IQueryable<Parent> SortParents(this IQueryable<Parent> parents, string orderByQueryString) 
    { 
        if (string.IsNullOrWhiteSpace(orderByQueryString)) 
            return parents.OrderBy(e => e.FirstName); 
            
        var orderParams = orderByQueryString.Trim().Split(','); 
        var propertyInfos = typeof(Parent).GetProperties(BindingFlags.Public | BindingFlags.Instance); 
        var orderQueryBuilder = new StringBuilder(); 
            
        foreach (var param in orderParams) 
        { 
            if (string.IsNullOrWhiteSpace(param)) 
                continue; 
                
            var propertyFromQueryName = param.Split(" ")[0]; 
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase)); 
                
            if (objectProperty == null) 
                continue; 
                
            var direction = param.EndsWith(" desc") ? "descending" : "ascending"; 
            orderQueryBuilder.Append($"{objectProperty.Name} {direction}, "); 
        } 
            
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); 
        if (string.IsNullOrWhiteSpace(orderQuery)) 
            return parents.OrderBy(e => e.FirstName); 
            
        return parents.OrderBy(orderQuery); 
    }
    
}