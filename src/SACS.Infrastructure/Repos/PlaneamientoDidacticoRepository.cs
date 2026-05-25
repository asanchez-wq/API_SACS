using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SACS.Infrastructure.Repos;

public class PlaneamientoDidacticoRepository : BaseReadRepository, IPlaneamientoDidacticoRepository
{
    public PlaneamientoDidacticoRepository(DbFactory db) : base(db) { }

    public async Task<(IEnumerable<PlaneamientoDidactico> rows, long total)> GetPagedAsync(
        int limit, int offset, string? orderBy, bool desc)
    {
        var orderCol = string.IsNullOrWhiteSpace(orderBy) ? "id_plan_didactico" : orderBy!;
        var dir = desc ? "DESC" : "ASC";
        var table = Db.FullTable("planeamiento_didactico");

        // columnas con comillas escapadas
        var selectCols =
            "t.\"id_plan_didactico\", t.\"id_colegio\", t.\"id_periodo\", t.\"id_nivel_academico\", " +
            "t.\"id_tipo_grado\", t.\"id_curso\", t.\"trimestre\", t.\"semana\", t.\"id_docente\", " +
            "t.\"fecha_inicio_semana\", t.\"fecha_fin_semana\", t.\"estado\", t.\"usuario_crea\", " +
            "t.\"fecha_creacion\", t.\"hora_crea\", t.\"usuario_mod\", t.\"fecha_mod\", t.\"hora_mod\"";

        var select = $"SELECT {selectCols} FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count = $"SELECT COUNT(*) FROM {table} t";

        return await QueryPagedAsync<PlaneamientoDidactico>(select, count, new { limit, offset });
    }

    public async Task<PlaneamientoDidactico?> GetByIdAsync(int id)
    {
        var table = Db.FullTable("planeamiento_didactico");

        var selectCols =
            "t.\"id_plan_didactico\", t.\"id_colegio\", t.\"id_periodo\", t.\"id_nivel_academico\", " +
            "t.\"id_tipo_grado\", t.\"id_curso\", t.\"trimestre\", t.\"semana\", t.\"id_docente\", " +
            "t.\"fecha_inicio_semana\", t.\"fecha_fin_semana\", t.\"estado\", t.\"usuario_crea\", " +
            "t.\"fecha_creacion\", t.\"hora_crea\", t.\"usuario_mod\", t.\"fecha_mod\", t.\"hora_mod\"";

        var sql = $"SELECT {selectCols} FROM {table} t WHERE t.{Db.Quote("id_plan_didactico")} = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidactico>(sql, new { id });
    }

    public async Task<(IEnumerable<PlaneamientoDidacticoDto> rows, long total)> GetPagedDtoAsync(
        int limit, int offset, string? orderBy, bool desc)
    {
        var orderCol = string.IsNullOrWhiteSpace(orderBy) ? "id_plan_didactico" : orderBy!;
        var dir = desc ? "DESC" : "ASC";
        var select = $@"SELECT 
            t.id_plan_didactico as IdPlanDidactico,
            t.semana as Semana,
            t.fecha_inicio_semana::date as FechaInicioSemana, t.fecha_fin_semana::date as FechaFinSemana,
            t.estado as Estado, t.usuario_crea as UsuarioCrea, t.fecha_creacion::date as FechaCreacion,
            t.hora_crea::time as HoraCrea, t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod,
            t.hora_mod::time as HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            na.descripcion AS NivelAcademicoDescripcion,
            tg.descripcion AS TipoGradoDescripcion,
            tr.descripcion AS TrimestreDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("planeamiento_didactico")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("niveles_academicos")} na ON t.id_nivel_academico = na.id_nivel_academico
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tg ON t.id_tipo_grado = tg.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.trimestre = tr.id_trimestre
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count = $"SELECT COUNT(*) FROM {Db.FullTable("planeamiento_didactico")} t";
        return await QueryPagedAsync<PlaneamientoDidacticoDto>(select, count, new { limit, offset });
    }

    public async Task<PlaneamientoDidacticoDto?> GetByIdDtoAsync(int id)
    {
        var sql = $@"SELECT 
            t.id_plan_didactico as IdPlanDidactico,
            t.semana as Semana,
            t.fecha_inicio_semana::date as FechaInicioSemana, t.fecha_fin_semana::date as FechaFinSemana,
            t.estado as Estado, t.usuario_crea as UsuarioCrea, t.fecha_creacion::date as FechaCreacion,
            t.hora_crea::time as HoraCrea, t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod,
            t.hora_mod::time as HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            na.descripcion AS NivelAcademicoDescripcion,
            tg.descripcion AS TipoGradoDescripcion,
            tr.descripcion AS TrimestreDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("planeamiento_didactico")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("niveles_academicos")} na ON t.id_nivel_academico = na.id_nivel_academico
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tg ON t.id_tipo_grado = tg.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.trimestre = tr.id_trimestre
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        WHERE t.id_plan_didactico = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoDto>(sql, new { id });
    }
}
