using System;

namespace SACS.Domain.DTOs;

public class AsistenciaDto
{
    public int? IdAsistencia { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string AlumnoNombres { get; set; }
    public string AlumnoApellidos { get; set; }
    public string AlumnoCodigo { get; set; }
    public string GrupoDescripcion { get; set; }
    public string CursoDescripcion { get; set; }
    public string DocenteNombres { get; set; }
    public string DocenteApellidos { get; set; }
    public DateTime? FechaAsistencia { get; set; }
    public string EstadoAsistencia { get; set; }
    public string EstadoProceso { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    public string TipoProcesado { get; set; }
}
