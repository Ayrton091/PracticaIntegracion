using System.ComponentModel.DataAnnotations;

public class Docente
{
    public int Id { get; set; }
    [Required]
    public string Apellidos { get; set; }
    [Required]
    public string Nombres { get; set; }
    public string Profesion { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Correo { get; set; }
}