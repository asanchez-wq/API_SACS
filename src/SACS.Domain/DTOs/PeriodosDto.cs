using System;

namespace SACS.Domain.DTOs;

public class PeriodosDto
{
    public int? IdPeriodo { get; set; }
    public string ColegioDescripcion { get; set; }
    public string Descripcion { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
}
