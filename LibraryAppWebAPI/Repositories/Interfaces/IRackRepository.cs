namespace LibraryAppWebAPI.Repositories.Interfaces;

using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;

public interface IRackRepository
{
    Task<IEnumerable<Rack>> GetAllAsync();
    Task<Rack?> GetByIdAsync(int id);
    Task<IEnumerable<Rack>> GetByFloorAsync(int floor);
    Task<IEnumerable<Rack>> GetBySectionAsync(Section section);
    Task<Rack> AddAsync(Rack rack);
    Task<Rack?> UpdateAsync(Rack rack);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
