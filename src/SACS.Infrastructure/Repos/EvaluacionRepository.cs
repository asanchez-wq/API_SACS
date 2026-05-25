using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class EvaluacionRepository : BaseReadRepository, IEvaluacionRepository
{
    public EvaluacionRepository(DbFactory db):base(db){}

    public async Task<(IEnumerable<Evaluacion> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_evaluacion":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$"SELECT * FROM {Db.FullTable("evaluacion")} ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("evaluacion")}";
        return await QueryPagedAsync<Evaluacion>(select,count,new{limit,offset});
    }

    public async Task<Evaluacion?> GetByIdAsync(int id){
        var sql=$"SELECT * FROM {Db.FullTable("evaluacion")} WHERE id_evaluacion=@id LIMIT 1";
        return await QuerySingleAsync<Evaluacion>(sql,new{id});
    }

    public async Task<(IEnumerable<EvaluacionDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_evaluacion":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_evaluacion AS IdEvaluacion,
            t.descripcion AS Descripcion, t.calificacion AS Calificacion, 
            t.fecha_asignacion::date AS FechaAsignacion, t.fecha_entrega::date AS FechaEntrega, 
            t.tipo_nota AS TipoNota, t.recurso AS Recurso, t.nombre_recurso AS NombreRecurso, t.tipo_recurso AS TipoRecurso,
            t.estado AS Estado, t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos,
            te.descripcion AS TipoEvaluacionDescripcion,
            tr.descripcion AS TrimestreDescripcion
        FROM {Db.FullTable("evaluacion")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.trimestre = tr.id_trimestre
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_evaluacion")} te ON t.id_tipo_evaluacion = te.id_tipo_evaluacion
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("evaluacion")} t";
        return await QueryPagedAsync<EvaluacionDto>(select,count,new{limit,offset});
    }

    public async Task<EvaluacionDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_evaluacion AS IdEvaluacion,
            t.descripcion AS Descripcion, t.calificacion AS Calificacion, 
            t.fecha_asignacion::date AS FechaAsignacion, t.fecha_entrega::date AS FechaEntrega, 
            t.tipo_nota AS TipoNota, t.recurso AS Recurso, t.nombre_recurso AS NombreRecurso, t.tipo_recurso AS TipoRecurso,
            t.estado AS Estado, t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos,
            te.descripcion AS TipoEvaluacionDescripcion,
            tr.descripcion AS TrimestreDescripcion
        FROM {Db.FullTable("evaluacion")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.trimestre = tr.id_trimestre
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_evaluacion")} te ON t.id_tipo_evaluacion = te.id_tipo_evaluacion
        WHERE t.id_evaluacion = @id LIMIT 1";
        return await QuerySingleAsync<EvaluacionDto>(sql,new{id});
    }
}
