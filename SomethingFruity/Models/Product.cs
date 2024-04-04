using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SomethingFruity.Models;

namespace SomethingFruity.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Code is required")]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        public byte[] Image { get; set; }
        public string ImageName { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public DateTime DateCreated { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }
    }
}
