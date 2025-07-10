using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Curso
{
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    public int Creditos { get; set; }
    public int HorasSemanal { get; set; }
    public int Ciclo { get; set; }

    [ForeignKey("Docente")]
    public int IdDocente { get; set; }
    public Docente Docente { get; set; }
}