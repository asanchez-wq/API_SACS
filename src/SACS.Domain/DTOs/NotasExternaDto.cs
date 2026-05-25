using System;

namespace SACS.Domain.DTOs;

public class NotasExternaDto
{
    public int? IdNotaExterna { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string GrupoDescripcion { get; set; }
    public string AlumnoNombres { get; set; }
    public string AlumnoApellidos { get; set; }
    public string AlumnoCodigo { get; set; }
    public string CursoDescripcion { get; set; }
    public string TrimestreDescripcion { get; set; }
    public decimal NotaSumativa { get; set; }
    public decimal NotaApreciativa { get; set; }
    public int? Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
}
