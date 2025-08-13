namespace LibraryAppWebAPI.Repositories;

using LibraryAppWebAPI.Data;
using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BorrowingRecordRepository(LibraryDbContext context) : IBorrowingRecordRepository
{
    private readonly LibraryDbContext _context = context;

    public async Task<IEnumerable<BorrowingRecord>> GetAllAsync()
    {
        return await _context.BorrowingRecords.ToListAsync();
    }

    public async Task<BorrowingRecord?> GetByIdAsync(int id)
    {
        return await _context.BorrowingRecords.FirstOrDefaultAsync(br => br.Id == id);
    }

    public async Task<IEnumerable<BorrowingRecord>> GetByMemberIdAsync(int memberId)
    {
        return await _context.BorrowingRecords.ToListAsync();
    }

    public async Task<IEnumerable<BorrowingRecord>> GetByBookIdAsync(int bookId)
    {
        return await _context.BorrowingRecords.ToListAsync();
    }

    public async Task<IEnumerable<BorrowingRecord>> GetByStatusAsync(Status status)
    {
        return await _context.BorrowingRecords.ToListAsync();
    }

    public async Task<BorrowingRecord> AddAsync(BorrowingRecord record)
    {
        _context.BorrowingRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<BorrowingRecord?> UpdateAsync(BorrowingRecord record)
    {
        var existingRecord = await _context.BorrowingRecords.FindAsync(record.Id);
        if (existingRecord == null)
            return null;

        _context.Entry(existingRecord).CurrentValues.SetValues(record);
        await _context.SaveChangesAsync();
        return existingRecord;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record = await _context.BorrowingRecords.FindAsync(id);
        if (record == null)
            return false;

        record.DeletedAt = new DateTime();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.BorrowingRecords.AnyAsync(br => br.Id == id);
    }
}
