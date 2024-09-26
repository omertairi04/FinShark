using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Stock
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    // decimal with 18 digits and 2 decimals
    [Column(TypeName = "decimal(18,2)")] 
    public decimal Purchase { get; set; }
    [Column(TypeName = "decimal(18,2)")] 
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    // one to many
    public List<Comment> Comments { get; set; } = new List<Comment>();
}