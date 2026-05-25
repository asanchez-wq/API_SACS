using System;

namespace SACS.Domain.DTOs;

public class PlaneamientoDidacticoDto
{
    // Campos base (estructura real de BD)
    public int IdPlanDidactico { get; set; }
    public int Semana { get; set; }
    public DateTime? FechaInicioSemana { get; set; }
    public DateTime? FechaFinSemana { get; set; }
    public int Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    
    // Campos expandidos (JOIN) - descripciones en lugar de IDs
    public string? ColegioDescripcion { get; set; }
    public string? PeriodoDescripcion { get; set; }
    public string? NivelAcademicoDescripcion { get; set; }
    public string? TipoGradoDescripcion { get; set; }
    public string? TrimestreDescripcion { get; set; }
    public string? CursoDescripcion { get; set; }
    public string? DocenteNombres { get; set; }
    public string? DocenteApellidos { get; set; }
}
