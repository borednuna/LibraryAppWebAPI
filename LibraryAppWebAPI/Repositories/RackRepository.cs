namespace LibraryAppWebAPI.Repositories;

using LibraryAppWebAPI.Data;
using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RackRepository(LibraryDbContext context) : IRackRepository
{
    private readonly LibraryDbContext _context = context;

    public async Task<IEnumerable<Rack>> GetAllAsync()
    {
        return await _context.Racks.ToListAsync();
    }

    public async Task<Rack?> GetByIdAsync(int id)
    {
        return await _context.Racks.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Rack>> GetByFloorAsync(int floor)
    {
        return await _context.Racks.ToListAsync();
    }

    public async Task<IEnumerable<Rack>> GetBySectionAsync(Section section)
    {
        return await _context.Racks.ToListAsync();
    }

    public async Task<Rack> AddAsync(Rack rack)
    {
        _context.Racks.Add(rack);
        await _context.SaveChangesAsync();
        return rack;
    }

    public async Task<Rack?> UpdateAsync(Rack rack)
    {
        var existingRack = await _context.Racks.FindAsync(rack.Id);
        if (existingRack == null)
            return null;

        existingRack.Floor = rack.Floor;
        existingRack.Section = rack.Section;
        existingRack.Capacity = rack.Capacity;

        await _context.SaveChangesAsync();
        return existingRack;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rack = await _context.Racks.FindAsync(id);
        if (rack == null)
            return false;

        rack.DeletedAt = new DateTime();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Racks.AnyAsync(r => r.Id == id);
    }
}