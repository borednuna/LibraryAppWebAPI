namespace LibraryAppWebAPI.Services;

using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories;
using LibraryAppWebAPI.Repositories.Interfaces;
using LibraryAppWebAPI.Services.Interfaces;

public class RackService(IRackRepository rackRepository, IBookRepository bookRepository) : IRackService
{
    private readonly IRackRepository _rackRepository = rackRepository;
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<IEnumerable<Rack>> GetAllRacksAsync()
    {
        return await _rackRepository.GetAllAsync();
    }

    public async Task<Rack?> GetRackByIdAsync(int id)
    {
        return await _rackRepository.GetByIdAsync(id);
    }

    public async Task<Rack> CreateRackAsync(Rack rack)
    {
        return await _rackRepository.AddAsync(rack);
    }

    public async Task<Rack?> UpdateRackAsync(Rack rack)
    {
        return await _rackRepository.UpdateAsync(rack);
    }

    public async Task<bool> DeleteRackAsync(int id)
    {
        return await _rackRepository.DeleteAsync(id);
    }

    // Business logic
    public async Task<IEnumerable<Rack>> GetRacksBySectionAsync(Section section)
    {
        var racks = await _rackRepository.GetAllAsync();
        return racks.Where(r => r.Section == section);
    }

    public async Task<IEnumerable<Rack>> GetRacksByFloorAsync(int floor)
    {
        var racks = await _rackRepository.GetAllAsync();
        return racks.Where(r => r.Floor == floor);
    }

    // public async Task<bool> IsRackFullAsync(int rackId)
    // {
    //     var rack = await _rackRepository.GetByIdAsync(rackId);
    //     if (rack == null)
    //         throw new InvalidOperationException("Rack not found.");

    //     var booksInRack = await _bookRepository.GetBooksInRackAsync(rackId);
    //     return booksInRack.Count() >= rack.Capacity;
    // }

    // public async Task<int> GetAvailableSpaceAsync(int rackId)
    // {
    //     var rack = await _rackRepository.GetByIdAsync(rackId);
    //     if (rack == null)
    //         throw new InvalidOperationException("Rack not found.");

    //     var booksInRack = await _bookRepository.GetBooksInRackAsync(rackId);
    //     return rack.Capacity - booksInRack.Count();
    // }
}
