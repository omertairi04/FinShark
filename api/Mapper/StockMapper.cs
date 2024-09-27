using api.Dtos.Stock;
using api.Models;

namespace api.Mapper;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock stock)
    {
        return new StockDto
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments
                .Select(c => c.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stock)
    {
        return new Stock
        {
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
        };
    }
    
}