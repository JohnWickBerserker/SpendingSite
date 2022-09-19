using System.ComponentModel.DataAnnotations;

namespace SpendingSite.Db.Entities
{
    public class SpendKind
    {
        [Key]
        public int SpendKindId { get; set; }
        public string Name { get; set; }
        public List<Spending> Spendings { get; set; }
    }
}
