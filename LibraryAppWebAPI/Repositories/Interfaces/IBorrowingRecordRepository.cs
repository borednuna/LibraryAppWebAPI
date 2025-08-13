namespace LibraryAppWebAPI.Repositories.Interfaces;

using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Enums;

public interface IBorrowingRecordRepository
{
    Task<IEnumerable<BorrowingRecord>> GetAllAsync();
    Task<BorrowingRecord?> GetByIdAsync(int id);
    Task<IEnumerable<BorrowingRecord>> GetByMemberIdAsync(int memberId);
    Task<IEnumerable<BorrowingRecord>> GetByBookIdAsync(int bookId);
    Task<IEnumerable<BorrowingRecord>> GetByStatusAsync(Status status);
    Task<BorrowingRecord> AddAsync(BorrowingRecord record);
    Task<BorrowingRecord?> UpdateAsync(BorrowingRecord record);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
