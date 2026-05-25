using System;

namespace SACS.Domain.DTOs;

public class ActividadesEventosDto
{
    // Campos base (estructura real de la BD)
    public int IdActividadesEventos { get; set; }
    public string? TipoAlcance { get; set; }
    public string? TituloEvento { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaEvento { get; set; }
    public TimeSpan? HoraInicial { get; set; }
    public TimeSpan? HoraFinal { get; set; }
    public string? Estado { get; set; }
    public string? UsuarioCrea { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public TimeSpan? HoraCrea { get; set; }
    public string? UsuarioMod { get; set; }
    public DateTime? FechaMod { get; set; }
    public TimeSpan? HoraMod { get; set; }
    
    // Campos expandidos (JOIN) - descripciones en lugar de IDs
    public string? ColegioDescripcion { get; set; }
    public string? PeriodoDescripcion { get; set; }
    public string? Nivel_Academico_Descripcion { get; set; }
    public string? Tipo_Grado_Descripcion { get; set; }
    public string? Tipo_Grupo_Descripcion { get; set; }
}
