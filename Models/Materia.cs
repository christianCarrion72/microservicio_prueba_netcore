using System.ComponentModel.DataAnnotations.Schema;

public class Materia
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    public string? Nombre { get; set; }

    [Column("codigo")]
    public string? Codigo { get; set; }

    [Column("idNivel")]
    public int IdNivel { get; set; }
}
