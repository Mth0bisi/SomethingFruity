using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SomethingFruity.Models
{
    public partial class User : IdentityUser
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {

    }

}

