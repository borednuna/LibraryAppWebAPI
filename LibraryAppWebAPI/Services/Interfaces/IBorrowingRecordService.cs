namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.Models;

public interface IBorrowingRecordService
{
    Task<IEnumerable<BorrowingRecord>> GetAllBorrowingRecordsAsync();
    Task<BorrowingRecord?> GetBorrowingRecordByIdAsync(int id);
    Task<BorrowingRecord> CreateBorrowingRecordAsync(BorrowingRecord record);
    Task<BorrowingRecord?> UpdateBorrowingRecordAsync(BorrowingRecord record);
    Task<bool> DeleteBorrowingRecordAsync(int id);
    // Task<BorrowingRecord> BorrowBookAsync(int memberId, int bookId, DateTime dueDate);
    // Task<BorrowingRecord> ReturnBookAsync(int borrowingRecordId, DateTime returnDate);
    // Task<decimal> CalculateFineAsync(int borrowingRecordId, decimal dailyFineAmount);
    // Task<IEnumerable<BorrowingRecord>> GetBorrowingRecordsByMemberAsync(int memberId);
    // Task<IEnumerable<BorrowingRecord>> GetBorrowingRecordsByBookAsync(int bookId);
    // Task<IEnumerable<BorrowingRecord>> GetOverdueRecordsAsync(DateTime currentDate);
};
 