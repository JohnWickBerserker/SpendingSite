namespace SpendingSite.Dto
{
    public class SpendingGroupDto
    {
        public SpendingGroupDto
            (string spendKindName, decimal amount, IEnumerable<(string? Note, decimal Amount)> items)
        {
            SpendKindName = spendKindName;
            Amount = amount;
            Items = items.ToList();
        }

        public string SpendKindName { get; set; }

        public decimal Amount { get; set; }

        public List<(string? Note, decimal Amount)> Items { get; set; }
            = new List<(string? Note, decimal Amount)>();
    }
}
