using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicMVC.Models
{
    public class Tutorial
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Please enter text")]
        [Required]
        [Display(Name = "Enter the name of tutorial")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Please describe your tutorial")]
        public string Description { get; set; }

        // Relationships
        public List<Article>? Articles { get; set; }

    }
}
