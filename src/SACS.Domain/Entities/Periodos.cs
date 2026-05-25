using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class Periodos
{
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    [Column("fecha_inicio")]
    public DateOnly? FechaInicio { get; set; }
    [Column("fecha_fin")]
    public DateOnly? FechaFin { get; set; }
    [Column("estado")]
    public int? Estado { get; set; }
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
