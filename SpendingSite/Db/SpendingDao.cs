using SpendingSite.Db.Entities;
using SpendingSite.Dto;

namespace SpendingSite.Db
{
    public class SpendingDao
    {
        public List<SpendingGroupDto> GetDaySpendings(DateTime date)
        {
            using (var db = new AppDbContext())
            {
                var queryDate = date.ToUniversalTime();
                var s = db.Spendings
                    .Where(x => x.SpendDate == queryDate)
                    .Select(x => new { x.SpendKind.Name, x.Note, x.Amount })
                    .ToList();
                var s2 = s
                    .GroupBy(x => x.Name)
                    .Select(x => new SpendingGroupDto(x.Key, x.Sum(y => y.Amount), x.Select(y => (y.Note, y.Amount))))
                    .ToList();
                var s3 = s2.OrderBy(x => x.SpendKindName).ToList();
                return s3.Select(x => new SpendingGroupDto(x.SpendKindName, x.Amount, x.Items)).ToList();
            }
        }

        public List<(string SpendKindName, decimal Amount)> GetMonthSpendings(DateTime selectedMonth)
        {
            using (var db = new AppDbContext())
            {
                var queryDateFrom = new DateTime(selectedMonth.Year, selectedMonth.Month, 1)
                    .ToUniversalTime();
                var queryDateTo = new DateTime(selectedMonth.Year, selectedMonth.Month, 1)
                    .AddMonths(1)
                    .ToUniversalTime();
                var s = db.Spendings
                    .Where(x => x.SpendDate >= queryDateFrom && x.SpendDate < queryDateTo)
                    .Select(x => new { x.SpendKind.Name, x.Amount })
                    .GroupBy(x => new { x.Name })
                    .Select(x => new { x.Key.Name, Amount = x.Sum(y => y.Amount) })
                    .ToList();
                s = s.OrderBy(x => x.Name).ToList();
                return s.Select(x => (x.Name, x.Amount)).ToList();
            }
        }

        public void AddSpending(decimal amount, int spendKindId, string? note, DateTime spendDate)
        {
            using (var db = new AppDbContext())
            {
                db.Spendings.Add(new Spending
                {
                    Amount = amount,
                    SpendKindId = spendKindId,
                    Note = note,
                    SpendDate = spendDate.Date.ToUniversalTime()
                });
                db.SaveChanges();
            }
        }
    }
}
