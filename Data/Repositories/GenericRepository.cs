#nullable disable
using Microsoft.EntityFrameworkCore;
using Spotify.Data.Repositories.Contracts;
namespace Spotify.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly SpotifyContext _context;

    public GenericRepository(SpotifyContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exist(int id)
    {
        T entity = await Get(id);
        return entity != null;
    }

    public async Task<T> Get(int id)
    {
        T entity = await _context.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        List<T> list = await _context.Set<T>().ToListAsync();
        return list;
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}