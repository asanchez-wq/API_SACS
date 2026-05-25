using System;

namespace SACS.Domain.DTOs;

public class PeriodosXHorariosDto
{
    public int? IdPeriodoXHorarios { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string GrupoDescripcion { get; set; }
    public string CursoDescripcion { get; set; }
    public string DocenteNombres { get; set; }
    public string DocenteApellidos { get; set; }
    public string Dia { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    public int? Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
}
