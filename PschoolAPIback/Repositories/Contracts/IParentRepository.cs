using PschoolAPIback.Models;

namespace PschoolAPIback.Repositories.Contracts;

public interface IParentRepository
{
    Task<IEnumerable<Parent>> GetItems();
    Task<Parent> GetItem(int id);
}