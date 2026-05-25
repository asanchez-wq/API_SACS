using System;

namespace SACS.Domain.DTOs;

public class PeriodosXGruposDto
{
    public int? IdPeriodoXGrupo { get; set; }
    public string ColegioDescripcion { get; set; }
    public string PeriodoDescripcion { get; set; }
    public string NivelAcademicoDescripcion { get; set; }
    public string TipoGradoDescripcion { get; set; }
    public string TipoGrupoDescripcion { get; set; }
    public int? Cupo { get; set; }
    public int? Estado { get; set; }
    public int? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public int? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    public string DocenteNombres { get; set; }
    public string DocenteApellidos { get; set; }
    public byte[] Recurso { get; set; }
    public string NombreRecurso { get; set; }
    public string TipoRecurso { get; set; }
}
