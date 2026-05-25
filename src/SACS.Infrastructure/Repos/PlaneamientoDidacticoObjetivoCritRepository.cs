using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class PlaneamientoDidacticoObjetivoCritRepository : BaseReadRepository, IPlaneamientoDidacticoObjetivoCritRepository
{
    public PlaneamientoDidacticoObjetivoCritRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<PlaneamientoDidacticoObjetivoCrit> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_plan_didactico_obj_crit":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("planeamiento_didactico_objetivo_crit");
        var select=$"SELECT t.id_plan_didactico_obj_crit, t.id_colegio, t.id_plan_didactico, t.id_plan_didactico_obj, t.numero_forma, t.criterio_forma, t.criterio_forma_des, t.criterio_fondo, t.usuario_crea, t.fecha_creacion, t.hora_crea FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<PlaneamientoDidacticoObjetivoCrit>(select,count,new{limit,offset});
    }
    public async Task<PlaneamientoDidacticoObjetivoCrit?> GetByIdAsync(int id){
        var table=Db.FullTable("planeamiento_didactico_objetivo_crit");
        var sql=$"SELECT t.id_plan_didactico_obj_crit, t.id_colegio, t.id_plan_didactico, t.id_plan_didactico_obj, t.numero_forma, t.criterio_forma, t.criterio_forma_des, t.criterio_fondo, t.usuario_crea, t.fecha_creacion, t.hora_crea FROM {table} t WHERE t.{Db.Quote("id_plan_didactico_obj_crit")} = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoObjetivoCrit>(sql,new{id});
    }

    public async Task<(IEnumerable<PlaneamientoDidacticoObjetivoCritDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_plan_didactico_obj_crit":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_plan_didactico_obj_crit AS IdPlanDidacticoObjCrit,
            col.descripcion AS ColegioDescripcion,
            t.numero_forma AS NumeroForma,
            t.criterio_forma AS CriterioForma,
            t.criterio_forma_des AS CriterioFormaDes,
            t.criterio_fondo AS CriterioFondo,
            t.usuario_crea AS UsuarioCrea,
            t.fecha_creacion::date AS FechaCreacion,
            t.hora_crea::time AS HoraCrea
        FROM {Db.FullTable("planeamiento_didactico_objetivo_crit")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("planeamiento_didactico_objetivo_crit")} t";
        return await QueryPagedAsync<PlaneamientoDidacticoObjetivoCritDto>(select,count,new{limit,offset});
    }

    public async Task<PlaneamientoDidacticoObjetivoCritDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_plan_didactico_obj_crit AS IdPlanDidacticoObjCrit,
            col.descripcion AS ColegioDescripcion,
            t.numero_forma AS NumeroForma,
            t.criterio_forma AS CriterioForma,
            t.criterio_forma_des AS CriterioFormaDes,
            t.criterio_fondo AS CriterioFondo,
            t.usuario_crea AS UsuarioCrea,
            t.fecha_creacion::date AS FechaCreacion,
            t.hora_crea::time AS HoraCrea
        FROM {Db.FullTable("planeamiento_didactico_objetivo_crit")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        WHERE t.id_plan_didactico_obj_crit = @id LIMIT 1";
        return await QuerySingleAsync<PlaneamientoDidacticoObjetivoCritDto>(sql,new{id});
    }
}

