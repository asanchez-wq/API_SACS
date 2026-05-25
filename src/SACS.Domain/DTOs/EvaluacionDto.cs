using System;

namespace SACS.Domain.DTOs;

public class EvaluacionDto
{
    public int? IdEvaluacion { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string GrupoDescripcion { get; set; }
    public string CursoDescripcion { get; set; }
    public string DocenteNombres { get; set; }
    public string DocenteApellidos { get; set; }
    public string TipoEvaluacionDescripcion { get; set; }
    public string TrimestreDescripcion { get; set; }
    public string Descripcion { get; set; }
    public decimal Calificacion { get; set; }
    public DateTime? FechaAsignacion { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string TipoNota { get; set; }
    public byte[] Recurso { get; set; }
    public string NombreRecurso { get; set; }
    public string TipoRecurso { get; set; }
    public string Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    public string TipoProcesado { get; set; }
}
