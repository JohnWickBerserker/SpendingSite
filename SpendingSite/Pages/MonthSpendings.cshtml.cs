using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpendingSite.Db;

namespace SpendingSite.Pages
{
    public class MonthSpendingsModel : PageModel
    {
        public string? SelectedMonth { get; set; }
        public string? SelectedMonthLocation { get; set; }

        public List<DateTime> AvailableMonthes { get; private set; } = new List<DateTime>();

        public void OnGet()
        {
            AvailableMonthes = GetAvailableMonthes();
            if (Request.Query.ContainsKey("month"))
            {
                SelectedMonth = Request.Query["month"][0];
                SelectedMonthLocation = $"MonthSpendings?month={SelectedMonth}";
            }
            else
            {
                SelectedMonth = $"{DateTime.Now.Year}{DateTime.Now.Month:D2}";
                SelectedMonthLocation = $"MonthSpendings?month={SelectedMonth}";
            }
        }

        public DateTime GetSelectedMonth()
        {
            try
            {
                if (SelectedMonth != null)
                {
                    var year = int.Parse(SelectedMonth.Substring(0, 4));
                    var month = int.Parse(SelectedMonth.Substring(4, 2));
                    return new DateTime(year, month, 1);
                }
                else
                {
                    return DateTime.Now;
                }
            }
            catch
            {
                return DateTime.Now;
            }
        }

        List<DateTime> GetAvailableMonthes()
        {
            using (var db = new AppDbContext())
            {
                var s = db.Spendings
                    .Select(x => x.SpendDate)
                    .GroupBy(x => new { x.Value.Month, x.Value.Year })
                    .Select(x => new { x.Key.Month, x.Key.Year })
                    .ToList();
                var dates = s.Select(x => new DateTime(x.Year, x.Month, 1)).ToList();
                var now = DateTime.Now;
                var currentMonth = new DateTime(now.Year, now.Month, 1);
                if (!dates.Contains(currentMonth))
                {
                    dates.Add(currentMonth);
                }
                return dates.OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
            }
        }
    }
}
