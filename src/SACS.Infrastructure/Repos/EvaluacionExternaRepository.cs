using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class EvaluacionExternaRepository : BaseReadRepository, IEvaluacionExternaRepository
{
    public EvaluacionExternaRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<EvaluacionExterna> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_evaluacion_externa":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("evaluacion_externa");
        var select=$"SELECT t.id_evaluacion_externa, t.id_colegio, t.id_herramientas_didactica, t.trimestre, t.descripcion, t.lexile, t.fecha_asignacion, t.fecha_entrega, t.recurso, t.nombre_recurso, t.tipo_recurso, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.grado_lectura FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<EvaluacionExterna>(select,count,new{limit,offset});
    }
    public async Task<EvaluacionExterna?> GetByIdAsync(int id){
        var table=Db.FullTable("evaluacion_externa");
        var sql=$"SELECT t.id_evaluacion_externa, t.id_colegio, t.id_herramientas_didactica, t.trimestre, t.descripcion, t.lexile, t.fecha_asignacion, t.fecha_entrega, t.recurso, t.nombre_recurso, t.tipo_recurso, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.grado_lectura FROM {table} t WHERE t.{Db.Quote("id_evaluacion_externa")} = @id LIMIT 1";
        return await QuerySingleAsync<EvaluacionExterna>(sql,new{id});
    }

    public async Task<(IEnumerable<EvaluacionExternaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_evaluacion_externa":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_evaluacion_externa as IdEvaluacionExterna, t.id_colegio as IdColegio, t.id_periodo as IdPeriodo,
            t.id_periodo_x_grupo as IdPeriodoXGrupo, t.id_herramientas_didactica as IdHerramientasDidactica,
            t.id_docente as IdDocente, t.trimestre as Trimestre, t.descripcion as Descripcion,
            t.lexile as Lexile, t.fecha_asignacion::date as FechaAsignacion, t.fecha_entrega::date as FechaEntrega,
            t.recurso as Recurso, t.nombre_recurso as NombreRecurso, t.tipo_recurso as TipoRecurso,
            t.estado as Estado, t.usuario_crea as UsuarioCrea, t.fecha_creacion::date as FechaCreacion,
            t.hora_crea::time as HoraCrea, t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod,
            t.hora_mod::time as HoraMod, t.grado_lectura as GradoLectura,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            hd.descripcion AS HerramientaDidacticaDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("evaluacion_externa")} t
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("herramientas_didactica")} hd ON t.id_herramientas_didactica = hd.id_herramientas_didactica
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("evaluacion_externa")} t";
        return await QueryPagedAsync<EvaluacionExternaDto>(select,count,new{limit,offset});
    }

    public async Task<EvaluacionExternaDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_evaluacion_externa as IdEvaluacionExterna, t.id_colegio as IdColegio, t.id_periodo as IdPeriodo,
            t.id_periodo_x_grupo as IdPeriodoXGrupo, t.id_herramientas_didactica as IdHerramientasDidactica,
            t.id_docente as IdDocente, t.trimestre as Trimestre, t.descripcion as Descripcion,
            t.lexile as Lexile, t.fecha_asignacion::date as FechaAsignacion, t.fecha_entrega::date as FechaEntrega,
            t.recurso as Recurso, t.nombre_recurso as NombreRecurso, t.tipo_recurso as TipoRecurso,
            t.estado as Estado, t.usuario_crea as UsuarioCrea, t.fecha_creacion::date as FechaCreacion,
            t.hora_crea::time as HoraCrea, t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod,
            t.hora_mod::time as HoraMod, t.grado_lectura as GradoLectura,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            hd.descripcion AS HerramientaDidacticaDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("evaluacion_externa")} t
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("herramientas_didactica")} hd ON t.id_herramientas_didactica = hd.id_herramientas_didactica
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        WHERE t.id_evaluacion_externa = @id LIMIT 1";
        return await QuerySingleAsync<EvaluacionExternaDto>(sql,new{id});
    }
}

