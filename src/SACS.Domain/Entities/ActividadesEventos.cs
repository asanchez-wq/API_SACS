using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class ActividadesEventos
{
    [Column("id_actividades_eventos")]
    public int? IdActividadesEventos { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_periodo_x_grupo")]
    public int? IdPeriodoXGrupo { get; set; }
    [Column("id_curso")]
    public int? IdCurso { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    [Column("fecha_evento")]
    public DateOnly? FechaEvento { get; set; }
    [Column("tipo_evento")]
    public string TipoEvento { get; set; }
    [Column("estado")]
    public string Estado { get; set; }
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
