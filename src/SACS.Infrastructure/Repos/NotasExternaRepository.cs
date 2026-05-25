using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class NotasExternaRepository : BaseReadRepository, INotasExternaRepository
{
    public NotasExternaRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<NotasExterna> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_notas_externa":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("notas_externa");
        var select=$"SELECT t.id_notas_externa, t.id_colegio, t.id_evaluacion_externa, t.puntaje, t.observacion, t.estado, t.tipo_procesado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<NotasExterna>(select,count,new{limit,offset});
    }
    public async Task<NotasExterna?> GetByIdAsync(int id){
        var table=Db.FullTable("notas_externa");
        var sql=$"SELECT t.id_notas_externa, t.id_colegio, t.id_evaluacion_externa, t.puntaje, t.observacion, t.estado, t.tipo_procesado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t WHERE t.{Db.Quote("id_notas_externa")} = @id LIMIT 1";
        return await QuerySingleAsync<NotasExterna>(sql,new{id});
    }

    public async Task<(IEnumerable<NotasExternaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_notas_externa":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_notas_externa AS IdNotaExterna, 
            t.puntaje AS NotaSumativa, t.observacion AS Observacion, t.estado AS Estado, t.tipo_procesado AS TipoProcesado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.codigo AS AlumnoCodigo,
            c.descripcion AS CursoDescripcion,
            tr.descripcion AS TrimestreDescripcion
        FROM {Db.FullTable("notas_externa")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("evaluacion_externa")} ee ON t.id_evaluacion_externa = ee.id_evaluacion_externa
        LEFT JOIN {Db.FullTable("periodos_x_horarios")} pxh ON ee.id_periodo_x_grupo = pxh.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON pxh.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON ee.trimestre = tr.id_trimestre
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("notas_externa")} t";
        return await QueryPagedAsync<NotasExternaDto>(select,count,new{limit,offset});
    }

    public async Task<NotasExternaDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_notas_externa AS IdNotaExterna, 
            t.puntaje AS NotaSumativa, t.observacion AS Observacion, t.estado AS Estado, t.tipo_procesado AS TipoProcesado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.codigo AS AlumnoCodigo,
            c.descripcion AS CursoDescripcion,
            tr.descripcion AS TrimestreDescripcion
        FROM {Db.FullTable("notas_externa")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("evaluacion_externa")} ee ON t.id_evaluacion_externa = ee.id_evaluacion_externa
        LEFT JOIN {Db.FullTable("periodos_x_horarios")} pxh ON ee.id_periodo_x_grupo = pxh.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON pxh.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON ee.trimestre = tr.id_trimestre
        WHERE t.id_notas_externa = @id LIMIT 1";
        return await QuerySingleAsync<NotasExternaDto>(sql,new{id});
    }
}
