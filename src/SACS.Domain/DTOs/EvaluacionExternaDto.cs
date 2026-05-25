using System;

namespace SACS.Domain.DTOs;

public class EvaluacionExternaDto
{
    // Campos base (estructura real de BD)
    public int IdEvaluacionExterna { get; set; }
    public int IdColegio { get; set; }
    public int IdHerramientasDidactica { get; set; }
    public int Trimestre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Lexile { get; set; }
    public DateTime? FechaAsignacion { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public byte[]? Recurso { get; set; }
    public string? NombreRecurso { get; set; }
    public string? TipoRecurso { get; set; }
    public string? Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    public int GradoLectura { get; set; }
    
    // Campos expandidos (JOIN) - descripciones en lugar de IDs
    public string? PeriodoDescripcion { get; set; }
    public string? GrupoDescripcion { get; set; }
    public string? HerramientaDidacticaDescripcion { get; set; }
    public string? DocenteNombres { get; set; }
    public string? DocenteApellidos { get; set; }
}
