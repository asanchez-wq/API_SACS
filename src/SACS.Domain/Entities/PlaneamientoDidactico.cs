using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class PlaneamientoDidactico
{
    [Column("id_plan_didactico")]
    public int? IdPlanDidactico { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_periodo_x_grupo")]
    public int? IdPeriodoXGrupo { get; set; }
    [Column("id_curso")]
    public int? IdCurso { get; set; }
    [Column("titulo")]
    public string Titulo { get; set; }
    [Column("contenido")]
    public string Contenido { get; set; }
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
