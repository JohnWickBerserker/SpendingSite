using System.ComponentModel.DataAnnotations;

namespace SpendingSite.InputModels
{
    public class SpendingModel : IValidatableObject
    {
        public decimal Amount { get; set; }

        public string? Note { get; set; }

        public int SpendKindId { get; set; }

        public DateTime SpendDate { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (Amount == 0)
            {
                yield return new ValidationResult($"Amount is required");
            }
            if (SpendKindId == 0)
            {
                yield return new ValidationResult($"SpendKind is required");
            }
            if (SpendDate == default)
            {
                yield return new ValidationResult($"Date is required");
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
