using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesCodeSpace.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "O campo {0} deve ter no máximo máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Document { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string FirstName { get; set; }

        [Display(Name = "Apelido")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string LastName { get; set; }

        [Display(Name = "Morada")]
        [MaxLength(200, ErrorMessage = "O campo {0} deve ter no máximo máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(20, ErrorMessage = "O campo {0} deve ter no máximo máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5196/images/noimage.png"
            : $"https://rafasaintssales2023.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Imagem")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "País")]
        [Range(1, int.MaxValue, ErrorMessage = "Deves selecionar um país.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "Estado/Localidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Deves seleccionar uma região/estado.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int StateId { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "Cidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Deves selecionar uma cidade.")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}