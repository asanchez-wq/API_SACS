using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class PeriodosXGruposRepository : BaseReadRepository, IPeriodosXGruposRepository
{
    public PeriodosXGruposRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<PeriodosXGrupos> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_periodo_x_grupo":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("periodos_x_grupos");
        var select=$"SELECT t.id_colegio, t.id_nivel_academico, t.id_tipo_grado, t.id_tipo_grupo, t.cupo, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.recurso, t.nombre_recurso, t.tipo_recurso FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<PeriodosXGrupos>(select,count,new{limit,offset});
    }
    public async Task<PeriodosXGrupos?> GetByIdAsync(int id){
        var table=Db.FullTable("periodos_x_grupos");
        var sql=$"SELECT t.id_colegio, t.id_nivel_academico, t.id_tipo_grado, t.id_tipo_grupo, t.cupo, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.recurso, t.nombre_recurso, t.tipo_recurso FROM {table} t WHERE t.{Db.Quote("id_periodo_x_grupo")} = @id LIMIT 1";
        return await QuerySingleAsync<PeriodosXGrupos>(sql,new{id});
    }

    public async Task<(IEnumerable<PeriodosXGruposDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_periodo_x_grupo":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_periodo_x_grupo AS IdPeriodoXGrupo,
            t.cupo AS Cupo, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            t.recurso AS Recurso, t.nombre_recurso AS NombreRecurso, t.tipo_recurso AS TipoRecurso,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            na.descripcion AS NivelAcademicoDescripcion,
            tg.descripcion AS TipoGradoDescripcion,
            tp.descripcion AS TipoGrupoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("periodos_x_grupos")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("niveles_academicos")} na ON t.id_nivel_academico = na.id_nivel_academico
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tg ON t.id_tipo_grado = tg.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tp ON t.id_tipo_grupo = tp.id_tipo_grupo
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("periodos_x_grupos")} t";
        return await QueryPagedAsync<PeriodosXGruposDto>(select,count,new{limit,offset});
    }

    public async Task<PeriodosXGruposDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_periodo_x_grupo AS IdPeriodoXGrupo,
            t.cupo AS Cupo, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            t.recurso AS Recurso, t.nombre_recurso AS NombreRecurso, t.tipo_recurso AS TipoRecurso,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            na.descripcion AS NivelAcademicoDescripcion,
            tg.descripcion AS TipoGradoDescripcion,
            tp.descripcion AS TipoGrupoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("periodos_x_grupos")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("niveles_academicos")} na ON t.id_nivel_academico = na.id_nivel_academico
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tg ON t.id_tipo_grado = tg.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tp ON t.id_tipo_grupo = tp.id_tipo_grupo
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        WHERE t.id_periodo_x_grupo = @id LIMIT 1";
        return await QuerySingleAsync<PeriodosXGruposDto>(sql,new{id});
    }
}

