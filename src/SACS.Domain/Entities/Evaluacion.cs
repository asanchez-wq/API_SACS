using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class Evaluacion
{
    [Column("id_evaluacion")]
    public int? IdEvaluacion { get; set; }
    [Column("id_colegio")]
    public int? IdColegio { get; set; }
    [Column("id_periodo")]
    public int? IdPeriodo { get; set; }
    [Column("id_periodo_x_grupo")]
    public int? IdPeriodoXGrupo { get; set; }
    [Column("id_curso")]
    public int? IdCurso { get; set; }
    [Column("id_docente")]
    public int? IdDocente { get; set; }
    [Column("id_tipo_evaluacion")]
    public int? IdTipoEvaluacion { get; set; }
    [Column("trimestre")]
    public int? Trimestre { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    [Column("calificacion")]
    public decimal Calificacion { get; set; }
    [Column("fecha_asignacion")]
    public DateOnly? FechaAsignacion { get; set; }
    [Column("fecha_entrega")]
    public DateOnly? FechaEntrega { get; set; }
    [Column("tipo_nota")]
    public string TipoNota { get; set; }
    [Column("recurso")]
    public byte[] Recurso { get; set; }
    [Column("nombre_recurso")]
    public string NombreRecurso { get; set; }
    [Column("tipo_recurso")]
    public string TipoRecurso { get; set; }
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
    [Column("tipo_procesado")]
    public string TipoProcesado { get; set; }
}
