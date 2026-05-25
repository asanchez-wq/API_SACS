using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class Matriculas
{
    [Column("id_matricula")]
    public int? IdMatricula { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_periodo_x_grupo")]
    public int? IdPeriodoXGrupo { get; set; }
    [Column("codigo")]
    public string Codigo { get; set; }
    [Column("id_alumno")]
    public int? IdAlumno { get; set; }
    [Column("situacion")]
    public string Situacion { get; set; }
    [Column("colegio_procedente")]
    public string ColegioProcedente { get; set; }
    [Column("repitente")]
    public int? Repitente { get; set; }
    [Column("estado")]
    public string Estado { get; set; }
    [Column("usuario_crea")]
    public int? UsuarioCrea { get; set; }
    [Column("fecha_creacion")]
    public DateOnly? FechaCreacion { get; set; }
    [Column("hora_crea")]
    public TimeSpan? HoraCrea { get; set; }
    [Column("usuario_mod")]
    public int? UsuarioMod { get; set; }
    [Column("fecha_mod")]
    public DateOnly? FechaMod { get; set; }
    [Column("hora_mod")]
    public TimeSpan? HoraMod { get; set; }
    [Column("monto_matricula")]
    public decimal? MontoMatricula { get; set; }
    [Column("observaciones")]
    public string Observaciones { get; set; }
    [Column("id_validacion_externo")]
    public Guid? IdValidacionExterno { get; set; }
}
