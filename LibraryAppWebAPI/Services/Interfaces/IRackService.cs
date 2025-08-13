namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Enums;

public interface IRackService
{
    Task<IEnumerable<Rack>> GetAllRacksAsync();
    Task<Rack?> GetRackByIdAsync(int id);
    Task<Rack> CreateRackAsync(Rack rack);
    Task<Rack?> UpdateRackAsync(Rack rack);
    Task<bool> DeleteRackAsync(int id);
    Task<IEnumerable<Rack>> GetRacksBySectionAsync(Section section);
    Task<IEnumerable<Rack>> GetRacksByFloorAsync(int floor);
    // Task<bool> IsRackFullAsync(int rackId);
    // Task<int> GetAvailableSpaceAsync(int rackId);
}
