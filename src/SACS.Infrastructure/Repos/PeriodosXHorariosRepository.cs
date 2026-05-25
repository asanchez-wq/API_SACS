using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class PeriodosXHorariosRepository : BaseReadRepository, IPeriodosXHorariosRepository
{
    public PeriodosXHorariosRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<PeriodosXHorarios> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_periodo_x_horarios":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("periodos_x_horarios");
        var select=$"SELECT t.id_colegio, t.dia, t.hora_inicio, t.hora_fin, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<PeriodosXHorarios>(select,count,new{limit,offset});
    }
    public async Task<PeriodosXHorarios?> GetByIdAsync(int id){
        var table=Db.FullTable("periodos_x_horarios");
        var sql=$"SELECT t.id_colegio, t.dia, t.hora_inicio, t.hora_fin, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t WHERE t.{Db.Quote("id_periodo_x_horarios")} = @id LIMIT 1";
        return await QuerySingleAsync<PeriodosXHorarios>(sql,new{id});
    }

    public async Task<(IEnumerable<PeriodosXHorariosDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_periodo_x_horarios":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_periodo_x_horarios AS IdPeriodoXHorarios,
            t.dia AS Dia, t.hora_inicio::time AS HoraInicio, t.hora_fin::time AS HoraFin, 
            t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("periodos_x_horarios")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("periodos_x_horarios")} t";
        return await QueryPagedAsync<PeriodosXHorariosDto>(select,count,new{limit,offset});
    }

    public async Task<PeriodosXHorariosDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_periodo_x_horarios AS IdPeriodoXHorarios,
            t.dia AS Dia, t.hora_inicio::time AS HoraInicio, t.hora_fin::time AS HoraFin, 
            t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("periodos_x_horarios")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        WHERE t.id_periodo_x_horarios = @id LIMIT 1";
        return await QuerySingleAsync<PeriodosXHorariosDto>(sql,new{id});
    }
}

