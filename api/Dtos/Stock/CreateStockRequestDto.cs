using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock;

public class CreateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Maximum length of Symbol is 10")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(10, ErrorMessage = "Maximum length of Company name is 10")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 100000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Maximum length of Industry name is 10")]
    public string Industry { get; set; } = string.Empty;
    [Range(1, 500000000000)]
    
    public long MarketCap { get; set; }
}