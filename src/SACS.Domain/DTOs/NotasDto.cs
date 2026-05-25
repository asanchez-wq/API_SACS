using System;

namespace SACS.Domain.DTOs;

public class NotasDto
{
    public int? IdNotas { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string EvaluacionDescripcion { get; set; }
    public string AlumnoNombres { get; set; }
    public string AlumnoApellidos { get; set; }
    public string AlumnoCodigo { get; set; }
    public string CursoDescripcion { get; set; }
    public decimal CalificacionSumativa { get; set; }
    public decimal CalificacionApreciativa { get; set; }
    public decimal NotaSumativa { get; set; }
    public decimal NotaApreciativa { get; set; }
    public string Observacion { get; set; }
    public string Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    public string TipoProcesado { get; set; }
}
