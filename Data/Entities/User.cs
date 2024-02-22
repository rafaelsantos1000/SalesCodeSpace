using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SalesCodeSpace.Enums;

namespace SalesCodeSpace.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Document { get; set; }

        [Display(Name = "Nomes")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? FirstName { get; set; }

        [Display(Name = "Apelidos")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? LastName { get; set; }

        [Display(Name = "Morada")]
        [MaxLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5196/images/noimage.png"
            : $"https://rafasaintssales2023.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Tipo de utilizador")]
        public UserType UserType { get; set; }

        [Display(Name = "Cidade")]
        public City? City { get; set; }

        [Display(Name = "Utilizador")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Utilizador")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }
}