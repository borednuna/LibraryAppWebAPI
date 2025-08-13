namespace LibraryAppWebAPI.Services;

using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories;
using LibraryAppWebAPI.Repositories.Interfaces;
using LibraryAppWebAPI.Services.Interfaces;

public class BorrowingRecordService(
    IBorrowingRecordRepository borrowingRecordRepository,
    IBookRepository bookRepository,
    IMemberRepository memberRepository) : IBorrowingRecordService
{
    private readonly IBorrowingRecordRepository _borrowingRecordRepository = borrowingRecordRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMemberRepository _memberRepository = memberRepository;

    // CRUD
    public async Task<IEnumerable<BorrowingRecord>> GetAllBorrowingRecordsAsync()
    {
        return await _borrowingRecordRepository.GetAllAsync();
    }

    public async Task<BorrowingRecord?> GetBorrowingRecordByIdAsync(int id)
    {
        return await _borrowingRecordRepository.GetByIdAsync(id);
    }

    public async Task<BorrowingRecord> CreateBorrowingRecordAsync(BorrowingRecord record)
    {
        return await _borrowingRecordRepository.AddAsync(record);
    }

    public async Task<BorrowingRecord?> UpdateBorrowingRecordAsync(BorrowingRecord record)
    {
        return await _borrowingRecordRepository.UpdateAsync(record);
    }

    public async Task<bool> DeleteBorrowingRecordAsync(int id)
    {
        return await _borrowingRecordRepository.DeleteAsync(id);
    }

    // Business logic
    // public async Task<BorrowingRecord> BorrowBookAsync(int memberId, int bookId, DateTime dueDate)
    // {
    //     // Validate member
    //     var member = await _memberRepository.GetByIdAsync(memberId);
    //     if (member == null || !member.IsActive)
    //         throw new InvalidOperationException("Member not found or inactive.");

    //     // Validate book
    //     var book = await _bookRepository.GetByIdAsync(bookId);
    //     if (book == null)
    //         throw new InvalidOperationException("Book not found.");

    //     // Check if already borrowed
    //     var existingRecord = (await _borrowingRecordRepository
    //         .GetBorrowingRecordsByBookAsync(bookId))
    //         .FirstOrDefault(r => r.Status == Status.BORROWED);

    //     if (existingRecord != null)
    //         throw new InvalidOperationException("Book is already borrowed.");

    //     // Create borrowing record
    //     var record = new BorrowingRecord
    //     {
    //         BookId = bookId,
    //         MemberId = memberId,
    //         BorrowDate = DateTime.UtcNow,
    //         DueDate = dueDate,
    //         Status = Status.BORROWED
    //     };

    //     return await _borrowingRecordRepository.AddAsync(record);
    // }

    // public async Task<BorrowingRecord> ReturnBookAsync(int borrowingRecordId, DateTime returnDate)
    // {
    //     var record = await _borrowingRecordRepository.GetByIdAsync(borrowingRecordId);
    //     if (record == null)
    //         throw new InvalidOperationException("Borrowing record not found.");

    //     if (record.Status != Status.BORROWED)
    //         throw new InvalidOperationException("Book is not currently borrowed.");

    //     record.ReturnDate = returnDate;
    //     record.Status = Status.AVAILABLE;

    //     // Optional fine calculation
    //     if (returnDate > record.DueDate)
    //     {
    //         var daysLate = (returnDate - record.DueDate).Days;
    //         record.FineAmount = daysLate > 0 ? daysLate * 1.50m : 0; // Example fine: $1.50/day
    //     }

    //     return await _borrowingRecordRepository.UpdateAsync(record);
    // }

    // public async Task<decimal> CalculateFineAsync(int borrowingRecordId, decimal dailyFineAmount)
    // {
    //     var record = await _borrowingRecordRepository.GetByIdAsync(borrowingRecordId);
    //     if (record == null)
    //         throw new InvalidOperationException("Borrowing record not found.");

    //     if (!record.ReturnDate.HasValue && DateTime.UtcNow > record.DueDate)
    //     {
    //         var daysLate = (DateTime.UtcNow - record.DueDate).Days;
    //         return daysLate * dailyFineAmount;
    //     }

    //     return 0;
    // }

    // // Queries
    // public async Task<IEnumerable<BorrowingRecord>> GetBorrowingRecordsByMemberAsync(int memberId)
    // {
    //     return await _borrowingRecordRepository.GetBorrowingRecordsByMemberAsync(memberId);
    // }

    // public async Task<IEnumerable<BorrowingRecord>> GetBorrowingRecordsByBookAsync(int bookId)
    // {
    //     return await _borrowingRecordRepository.GetBorrowingRecordsByBookAsync(bookId);
    // }

    // public async Task<IEnumerable<BorrowingRecord>> GetOverdueRecordsAsync(DateTime currentDate)
    // {
    //     var records = await _borrowingRecordRepository.GetAllAsync();
    //     return records.Where(r => r.Status == Status.BORROWED && r.DueDate < currentDate);
    // }
};
