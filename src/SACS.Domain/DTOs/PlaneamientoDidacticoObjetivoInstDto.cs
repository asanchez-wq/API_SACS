using System;

namespace SACS.Domain.DTOs;

public class PlaneamientoDidacticoObjetivoInstDto
{
    public int IdPlanDidacticoObjInst { get; set; }
    public string? ColegioDescripcion { get; set; }
    public string? TipoDetalle { get; set; }
    public int? Instrumento { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
}
