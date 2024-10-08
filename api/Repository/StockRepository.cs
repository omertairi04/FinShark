using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository : IStockRepository
{
    // dependency injection
    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _context.Stocks
            .Include(c => c.Comments)
            .ThenInclude(a => a.AppUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                // if true                                   // if false
                stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }

            if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
            {
                // if true                                   // if false
                stocks = query.IsDescending
                    ? stocks.OrderByDescending(s => s.CompanyName)
                    : stocks.OrderBy(s => s.CompanyName);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock?> GetBySymbolAsync(string symbol)
    {
        return await _context.Stocks.FirstOrDefaultAsync(
            x => x.Symbol == symbol);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(
            x => x.Id == id);
        if (existingStock == null) return null;
        existingStock.Symbol = stockDto.Symbol;
        existingStock.CompanyName = stockDto.CompanyName;
        existingStock.Purchase = stockDto.Purchase;
        existingStock.LastDiv = stockDto.LastDiv;
        existingStock.Industry = stockDto.Industry;
        existingStock.MarketCap = stockDto.MarketCap;

        await _context.SaveChangesAsync();
        return existingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null) return null;
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<bool> StockExists(int id)
    {
        return await _context.Stocks.AnyAsync(x => x.Id == id);
    }
}