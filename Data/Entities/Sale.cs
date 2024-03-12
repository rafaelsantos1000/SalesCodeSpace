using System.ComponentModel.DataAnnotations;
using SalesCodeSpace.Data.Entities;

namespace SalesCodeSpace.Data.Entities;

public class Sale
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
    [Display(Name = "Inventario")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime Date { get; set; }

    public User User { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Comentários")]
    public string Remarks { get; set; }

    [Display(Name = "Estado da encomenda")]
    public OrderStatus OrderStatus { get; set; }

    public ICollection<SaleDetail> SaleDetails { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}")]
    [Display(Name = "Linhas")]
    public int Lines => SaleDetails == null ? 0 : SaleDetails.Count;

    [DisplayFormat(DataFormatString = "{0:N2}")]
    [Display(Name = "Quantidade")]
    public float Quantity => SaleDetails == null ? 0 : SaleDetails.Sum(sd => sd.Quantity);

    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Display(Name = "Valor")]
    public decimal Value => SaleDetails == null ? 0 : SaleDetails.Sum(sd => sd.Value);

}
