using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class PlaneamientoDidacticoObjetivoInst
{
    [Column("id_plan_didactico_obj_inst")]
    public int? IdPlanDidacticoObjInst { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_plan_didactico")]
    public int? IdPlanDidactico { get; set; }
    [Column("id_plan_didactico_obj")]
    public int? IdPlanDidacticoObj { get; set; }
    [Column("tipo_detalle")]
    public string TipoDetalle { get; set; }
    [Column("instrumento")]
    public int? Instrumento { get; set; }
    [Column("usuario_crea")]
    public int? UsuarioCrea { get; set; }
    [Column("fecha_creacion")]
    public DateOnly? FechaCreacion { get; set; }
    [Column("hora_crea")]
    public TimeSpan? HoraCrea { get; set; }
}
