using System.ComponentModel.DataAnnotations;

namespace ContactsCatalog.Models
{
    public class CustomerItemDTO
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Display(Name = "Nummer")]
        [RegularExpression(@"\b[A-Z0-9]([0-9]+)")]
        [StringLength(12, MinimumLength = 1)]
        [Required]
        public string? Number { get; set; }
        public ICollection<Contact>? Contacts { get; set; } //details
    }

}
