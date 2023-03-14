using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsCatalog.Models
{
    public class CustomerItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Number { get; set; }
        public string? SecretUIDNumber { get; set; }

        public ICollection<Contact>? Contacts { get; set; } //details
    }
}
