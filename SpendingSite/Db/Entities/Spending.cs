using System.ComponentModel.DataAnnotations;

namespace SpendingSite.Db.Entities
{
    public class Spending
    {
        [Key]
        public long SpendId { get; set; }
        public int SpendKindId { get; set; }
        public DateTime? SpendDate { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public SpendKind SpendKind { get; set; }
    }
}
