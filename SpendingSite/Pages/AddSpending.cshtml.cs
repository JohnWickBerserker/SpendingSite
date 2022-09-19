using Microsoft.AspNetCore.Mvc.RazorPages;
using SpendingSite.Db;
using SpendingSite.InputModels;

namespace SpendingSite.Pages
{
    public class AddSpendingModel : PageModel
    {
        public string? ErrorMessage { get; set; }

        public void OnPostSubmit(SpendingModel spending)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = spending.Validate().FirstOrDefault()?.ErrorMessage;
                return;
            }
            new SpendingDao().AddSpending(spending.Amount, spending.SpendKindId, spending.Note, DateTime.Now);
            ErrorMessage = "Submitted!";
        }
    }
}
