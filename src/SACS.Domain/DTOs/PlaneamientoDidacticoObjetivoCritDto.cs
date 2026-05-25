using System;

namespace SACS.Domain.DTOs;

public class PlaneamientoDidacticoObjetivoCritDto
{
    public int IdPlanDidacticoObjCrit { get; set; }
    public string? ColegioDescripcion { get; set; }
    public int? NumeroForma { get; set; }
    public int? CriterioForma { get; set; }
    public string? CriterioFormaDes { get; set; }
    public string? CriterioFondo { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
}
