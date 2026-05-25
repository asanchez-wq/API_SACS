using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class PlaneamientoDidacticoObjetivoInstRepository : BaseReadRepository, IPlaneamientoDidacticoObjetivoInstRepository
{
    public PlaneamientoDidacticoObjetivoInstRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<PlaneamientoDidacticoObjetivoInst> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_plan_didactico_obj_inst":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("planeamiento_didactico_objetivo_inst");
        var select=$"SELECT t.id_plan_didactico_obj_inst, t.id_colegio, t.id_plan_didactico, t.id_plan_didactico_obj, t.tipo_detalle, t.instrumento, t.usuario_crea, t.fecha_creacion, t.hora_crea FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<PlaneamientoDidacticoObjetivoInst>(select,count,new{limit,offset});
    }
    public async Task<PlaneamientoDidacticoObjetivoInst?> GetByIdAsync(int id){
        var table=Db.FullTable("planeamiento_didactico_objetivo_inst");
        var sql=$"SELECT t.id_plan_didactico_obj_inst, t.id_colegio, t.id_plan_didactico, t.id_plan_didactico_obj, t.tipo_detalle, t.instrumento, t.usuario_crea, t.fecha_creacion, t.hora_crea FROM {table} t WHERE t.{Db.Quote("id_plan_didactico_obj_inst")} = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoObjetivoInst>(sql,new{id});
    }

    public async Task<(IEnumerable<PlaneamientoDidacticoObjetivoInstDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_plan_didactico_obj_inst":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_plan_didactico_obj_inst AS IdPlanDidacticoObjInst,
            col.descripcion AS ColegioDescripcion,
            t.tipo_detalle AS TipoDetalle,
            t.instrumento AS Instrumento,
            t.usuario_crea AS UsuarioCrea,
            t.fecha_creacion::date AS FechaCreacion,
            t.hora_crea::time AS HoraCrea
        FROM {Db.FullTable("planeamiento_didactico_objetivo_inst")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("planeamiento_didactico_objetivo_inst")} t";
        return await QueryPagedAsync<PlaneamientoDidacticoObjetivoInstDto>(select,count,new{limit,offset});
    }

    public async Task<PlaneamientoDidacticoObjetivoInstDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_plan_didactico_obj_inst AS IdPlanDidacticoObjInst,
            col.descripcion AS ColegioDescripcion,
            t.tipo_detalle AS TipoDetalle,
            t.instrumento AS Instrumento,
            t.usuario_crea AS UsuarioCrea,
            t.fecha_creacion::date AS FechaCreacion,
            t.hora_crea::time AS HoraCrea
        FROM {Db.FullTable("planeamiento_didactico_objetivo_inst")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        WHERE t.id_plan_didactico_obj_inst = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoObjetivoInstDto>(sql,new{id});
    }
}

