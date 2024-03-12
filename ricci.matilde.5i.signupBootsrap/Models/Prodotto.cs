using System.ComponentModel.DataAnnotations;

public class Prodotto
{
    [Key]
    public int Id { get; set; }
    public string? Articolo { get; set; }
    public int Quantita { get; set; }
}
    