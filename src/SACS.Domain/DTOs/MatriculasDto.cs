using System;

namespace SACS.Domain.DTOs;

public class MatriculasDto
{
    public int? IdMatricula { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string TipoGradoDescripcion { get; set; }
    public string GrupoDescripcion { get; set; }
    public string Codigo { get; set; }
    public string AlumnoNombres { get; set; }
    public string AlumnoApellidos { get; set; }
    public string AlumnoIdentificacion { get; set; }
    public string Situacion { get; set; }
    public string ColegioProcedente { get; set; }
    public int? Repitente { get; set; }
    public string Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    public decimal? MontoMatricula { get; set; }
    public string Observaciones { get; set; }
    public Guid? IdValidacionExterno { get; set; }
}
