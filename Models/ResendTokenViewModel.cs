using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class ResendTokenViewModel
    {
        [Display(Name = "Email")]
        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }

}