using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class PlaneamientoDidacticoObjetivoRepository : BaseReadRepository, IPlaneamientoDidacticoObjetivoRepository
{
    public PlaneamientoDidacticoObjetivoRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<PlaneamientoDidacticoObjetivo> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_plan_didactico_obj":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("planeamiento_didactico_objetivo");
        var select=$"SELECT t.id_plan_didactico_obj, t.id_colegio, t.id_plan_didactico, t.objetivo, t.indicadores, t.evidencia, t.usuario_crea, t.fecha_creacion, t.hora_crea FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<PlaneamientoDidacticoObjetivo>(select,count,new{limit,offset});
    }
    public async Task<PlaneamientoDidacticoObjetivo?> GetByIdAsync(int id){
        var table=Db.FullTable("planeamiento_didactico_objetivo");
        var sql=$"SELECT t.id_plan_didactico_obj, t.id_colegio, t.id_plan_didactico, t.objetivo, t.indicadores, t.evidencia, t.usuario_crea, t.fecha_creacion, t.hora_crea FROM {table} t WHERE t.{Db.Quote("id_plan_didactico_obj")} = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoObjetivo>(sql,new{id});
    }

    public async Task<(IEnumerable<PlaneamientoDidacticoObjetivoDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_plan_didactico_obj":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_plan_didactico_obj AS IdPlanDidacticoObj,
            col.descripcion AS ColegioDescripcion,
            t.objetivo AS Objetivo,
            t.indicadores AS Indicadores,
            t.evidencia AS Evidencia,
            t.usuario_crea AS UsuarioCrea,
            t.fecha_creacion::date AS FechaCreacion,
            t.hora_crea::time AS HoraCrea
        FROM {Db.FullTable("planeamiento_didactico_objetivo")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("planeamiento_didactico_objetivo")} t";
        return await QueryPagedAsync<PlaneamientoDidacticoObjetivoDto>(select,count,new{limit,offset});
    }

    public async Task<PlaneamientoDidacticoObjetivoDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_plan_didactico_obj AS IdPlanDidacticoObj,
            col.descripcion AS ColegioDescripcion,
            t.objetivo AS Objetivo,
            t.indicadores AS Indicadores,
            t.evidencia AS Evidencia,
            t.usuario_crea AS UsuarioCrea,
            t.fecha_creacion::date AS FechaCreacion,
            t.hora_crea::time AS HoraCrea
        FROM {Db.FullTable("planeamiento_didactico_objetivo")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        WHERE t.id_plan_didactico_obj = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoObjetivoDto>(sql,new{id});
    }
}

