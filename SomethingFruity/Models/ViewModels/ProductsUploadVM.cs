using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace SomethingFruity.Models.ViewModels
{
    public class ProductsUploadVM
    {
        [Display(Name = "Products File")]
        public string ProductsFile { get; set; }
    }

    public class ProductsVM
    {
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Code is required")]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Upload)]
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public byte[] DbImage { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
