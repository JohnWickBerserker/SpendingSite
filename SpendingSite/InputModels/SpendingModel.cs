using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SpendingSite.InputModels
{
    public class SpendingModel : IValidatableObject
    {
        [BindProperty]
        [Required]
        public decimal Amount { get; set; }

        [BindProperty]
        public string? Note { get; set; }

        [BindProperty]
        [Required]
        public int SpendKindId { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (Amount == 0)
            {
                yield return new ValidationResult($"Fill Amount");
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Validate();
        }
    }
}
