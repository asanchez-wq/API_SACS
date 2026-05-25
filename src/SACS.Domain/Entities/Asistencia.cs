using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class Asistencia
{
    [Column("id_asistencia")]
    public int? IdAsistencia { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_matricula")]
    public int? IdMatricula { get; set; }
    [Column("id_periodo_x_grupo")]
    public int? IdPeriodoXGrupo { get; set; }
    [Column("id_periodo_x_horarios")]
    public int? IdPeriodoXHorarios { get; set; }
    [Column("fecha_asistencia")]
    public DateOnly? FechaAsistencia { get; set; }
    [Column("estado_asistencia")]
    public string EstadoAsistencia { get; set; }
    [Column("estado_proceso")]
    public string EstadoProceso { get; set; }
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
    [Column("tipo_procesado")]
    public string TipoProcesado { get; set; }
}
