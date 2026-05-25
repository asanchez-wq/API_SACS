using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class Citaciones
{
    [Column("id_citacion")]
    public int? IdCitacion { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_matricula")]
    public int? IdMatricula { get; set; }
    [Column("fecha_citacion")]
    public DateOnly? FechaCitacion { get; set; }
    [Column("motivo")]
    public string Motivo { get; set; }
    [Column("observacion")]
    public string Observacion { get; set; }
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
