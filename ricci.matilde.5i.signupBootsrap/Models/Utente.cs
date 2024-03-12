using System.ComponentModel.DataAnnotations;

public class Utente
{
    [Key]
    public string? Email { get; set; }
    public string? Password { get; set; }
}