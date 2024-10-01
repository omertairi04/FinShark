using api.Models;

namespace api.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolios(AppUser user);
    Task<Portfolio> CreateAsync(Portfolio portfolio);
    Task<Portfolio> DeletePortfolio(AppUser user, string symbol);
}