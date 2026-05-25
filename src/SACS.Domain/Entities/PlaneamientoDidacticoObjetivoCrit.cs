using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class PlaneamientoDidacticoObjetivoCrit
{
    [Column("id_plan_didactico_obj_crit")]
    public int? IdPlanDidacticoObjCrit { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_plan_didactico")]
    public int? IdPlanDidactico { get; set; }
    [Column("id_plan_didactico_obj")]
    public int? IdPlanDidacticoObj { get; set; }
    [Column("numero_forma")]
    public int? NumeroForma { get; set; }
    [Column("criterio_forma")]
    public int? CriterioForma { get; set; }
    [Column("criterio_forma_des")]
    public string CriterioFormaDes { get; set; }
    [Column("criterio_fondo")]
    public string CriterioFondo { get; set; }
    [Column("usuario_crea")]
    public int? UsuarioCrea { get; set; }
    [Column("fecha_creacion")]
    public DateOnly? FechaCreacion { get; set; }
    [Column("hora_crea")]
    public TimeSpan? HoraCrea { get; set; }
}
