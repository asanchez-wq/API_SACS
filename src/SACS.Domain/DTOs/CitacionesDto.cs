using System;

namespace SACS.Domain.DTOs;

public class CitacionesDto
{
    // Campos base (estructura real de BD)
    public int IdCitaciones { get; set; }
    public int IdColegio { get; set; }
    public int IdHabitosActitudes { get; set; }
    public int IdTrimestre { get; set; }
    public string? Descripcion { get; set; }
    public string? NivelUrgencia { get; set; }
    public DateTime? FechaCitacion { get; set; }
    public DateTime? FechaRevision { get; set; }
    public TimeSpan? HoraRevision { get; set; }
    public int? IdApoderadoRevision { get; set; }
    public string? Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    
    // Campos expandidos (JOIN) - descripciones en lugar de IDs
    public string? PeriodoDescripcion { get; set; }
    public string? GrupoDescripcion { get; set; }
    public string? AlumnoNombres { get; set; }
    public string? AlumnoApellidos { get; set; }
    public string? DocenteNombres { get; set; }
    public string? DocenteApellidos { get; set; }
    public string? HabitoActitudDescripcion { get; set; }
    public string? TrimestreDescripcion { get; set; }
}
