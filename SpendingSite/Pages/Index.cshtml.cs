using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpendingSite.Db;
using System.Globalization;

namespace SpendingSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string? SelectedDay { get; set; }
        public string? SelectedDayLocation { get; set; }

        public List<DateTime> AvailableDays { get; private set; } = new List<DateTime>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            AvailableDays = GetAvailableDays();
            if (Request.Query.ContainsKey("day"))
            {
                SelectedDay = Request.Query["day"][0];
                SelectedDayLocation = $"?day={SelectedDay}";
            }
            else
            {
                SelectedDay = $"{DateTime.Now:dd.MM.yyyy}";
                SelectedDayLocation = $"?day={SelectedDay}";
            }
        }

        public DateTime GetSelectedDay()
        {
            try
            {
                if (SelectedDay != null)
                {
                    return DateTime.ParseExact(SelectedDay, "dd.MM.yyyy", CultureInfo.InvariantCulture);
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

        List<DateTime> GetAvailableDays()
        {
            using (var db = new AppDbContext())
            {
                var queryDate = DateTime.Now.AddDays(-30).ToUniversalTime();
                var dates = db.Spendings
                    .Where(x => x.SpendDate != null)
                    .Where(x => x.SpendDate > queryDate)
                    .Select(x => x.SpendDate.Value)
                    .Distinct()
                    .ToList();
                var now = DateTime.Now;
                var currentDay = new DateTime(now.Year, now.Month, now.Day);
                if (!dates.Contains(currentDay))
                {
                    dates.Add(currentDay);
                }
                return dates.OrderByDescending(x => x).ToList();
            }
        }
    }
}