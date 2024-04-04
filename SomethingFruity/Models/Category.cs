using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SomethingFruity.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category Code is required")]
        [Display(Name = "Category Code")]
        [CategoryCode(ErrorMessage = "Category code must contain 3 letters followed by 3 digits (e.g., ABC123).")]
        public string CategoryCode { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }


        public ICollection<Product> Products { get; set; }
    }

    public class CategoryCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Skip validation if value is null
            }

            string categoryCode = value.ToString();

            // Check if the category code matches the required format (3 letters + 3 digits)
            if (!Regex.IsMatch(categoryCode, @"^[a-zA-Z]{3}\d{3}$"))
            {
                return new ValidationResult("Category code must contain 3 letters followed by 3 digits (e.g., ABC123).");
            }

            return ValidationResult.Success;
        }
    }
}
