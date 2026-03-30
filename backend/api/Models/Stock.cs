using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal LastDividend { get; set; }

        public List<Comment> Comments { get; set; } = [];

        public List<Portfolio> Protfolios { get; set; } = [];
    }
}
