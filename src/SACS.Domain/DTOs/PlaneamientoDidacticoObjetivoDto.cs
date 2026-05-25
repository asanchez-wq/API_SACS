using System;

namespace SACS.Domain.DTOs;

public class PlaneamientoDidacticoObjetivoDto
{
    public int IdPlanDidacticoObj { get; set; }
    public string? ColegioDescripcion { get; set; }
    public string? Objetivo { get; set; }
    public string? Indicadores { get; set; }
    public string? Evidencia { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
}
