using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models;

public class EditTemporalSaleViewModel
{
    public int Id { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Comentários")]
    public string Remarks { get; set; }

    [DisplayFormat(DataFormatString = "{0:N2}")]
    [Display(Name = "Quantidade")]
    [Range(0.0000001, float.MaxValue, ErrorMessage = "Deve inserir um valor maior que zero na quantidade.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public float Quantity { get; set; }

}
