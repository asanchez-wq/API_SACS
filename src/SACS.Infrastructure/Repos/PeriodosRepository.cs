using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class PeriodosRepository : BaseReadRepository, IPeriodosRepository
{
    public PeriodosRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<Periodos> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_periodo":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("periodos");
        var select=$"SELECT t.id_colegio, t.descripcion, t.fecha_inicio, t.fecha_fin, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<Periodos>(select,count,new{limit,offset});
    }
    public async Task<Periodos?> GetByIdAsync(int id){
        var table=Db.FullTable("periodos");
        var sql=$"SELECT t.id_colegio, t.descripcion, t.fecha_inicio, t.fecha_fin, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t WHERE t.{Db.Quote("id_periodo")} = @id LIMIT 1";
        return await QuerySingleAsync<Periodos>(sql,new{id});
    }

    public async Task<(IEnumerable<PeriodosDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"t.id_periodo":"t."+orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_periodo AS IdPeriodo,
            t.descripcion AS Descripcion, 
            t.fecha_inicio::date AS FechaInicio, t.fecha_fin::date AS FechaFin, 
            t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion
        FROM {Db.FullTable("periodos")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("periodos")} t";
        return await QueryPagedAsync<PeriodosDto>(select,count,new{limit,offset});
    }

    public async Task<PeriodosDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_periodo AS IdPeriodo,
            t.descripcion AS Descripcion, 
            t.fecha_inicio::date AS FechaInicio, t.fecha_fin::date AS FechaFin, 
            t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion
        FROM {Db.FullTable("periodos")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        WHERE t.id_periodo = @id LIMIT 1";
        return await QuerySingleAsync<PeriodosDto>(sql,new{id});
    }
}

