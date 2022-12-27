using Microsoft.EntityFrameworkCore;
using PschoolAPIback.DbPschoolContext;
using PschoolAPIback.Models;
using PschoolAPIback.Repositories.Contracts;

namespace PschoolAPIback.Repositories;

public class ParentRepository : IParentRepository
{
    private readonly PschoolContext pschoolContext;

    public ParentRepository(PschoolContext pschoolContext )
    {
        this.pschoolContext = pschoolContext;
    }
    public async Task<IEnumerable<Parent>> GetItems()
    {
        var parents = await this.pschoolContext.Parents.ToListAsync();
        return parents; 
    }

    public async Task<Parent> GetItem(int id)
    {
        var parent = await this.pschoolContext.Parents.FindAsync(id);
        return parent;
    }
}