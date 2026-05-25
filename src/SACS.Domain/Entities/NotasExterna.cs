using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class NotasExterna
{
    [Column("id_notas_externa")]
    public int? IdNotasExterna { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_periodo_x_grupo")]
    public int? IdPeriodoXGrupo { get; set; }
    [Column("id_evaluacion_externa")]
    public int? IdEvaluacionExterna { get; set; }
    [Column("id_matricula")]
    public int? IdMatricula { get; set; }
    [Column("puntaje")]
    public decimal Puntaje { get; set; }
    [Column("observacion")]
    public string Observacion { get; set; }
    [Column("estado")]
    public string Estado { get; set; }
    [Column("tipo_procesado")]
    public string TipoProcesado { get; set; }
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
}
