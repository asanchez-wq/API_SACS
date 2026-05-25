using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SACS.Infrastructure.Repos;
public class ActividadesEventosRepository : BaseReadRepository, IActividadesEventosRepository
{
    public ActividadesEventosRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<ActividadesEventos> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_actividad_evento":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("actividades_eventos");
        var select=$"SELECT t.id_actividad_evento, t.id_colegio, t.tipo_alcance, t.id_nivel_academico, t.id_tipo_grado, t.id_tipo_grupo, t.titulo_evento, t.descripcion, t.fecha_evento, t.hora_inicial, t.hora_final, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<ActividadesEventos>(select,count,new{limit,offset});
    }
    public async Task<ActividadesEventos?> GetByIdAsync(int id){
        var table=Db.FullTable("actividades_eventos");
        var sql=$"SELECT t.id_actividad_evento, t.id_colegio, t.tipo_alcance, t.id_nivel_academico, t.id_tipo_grado, t.id_tipo_grupo, t.titulo_evento, t.descripcion, t.fecha_evento, t.hora_inicial, t.hora_final, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t WHERE t.{Db.Quote("id_actividad_evento")} = @id LIMIT 1";
        return await QuerySingleAsync<ActividadesEventos>(sql,new{id});
    }

    public async Task<(IEnumerable<ActividadesEventosDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_actividad_evento":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_actividad_evento as IdActividadesEventos,
            t.tipo_alcance as TipoAlcance, t.titulo_evento as TituloEvento, 
            t.descripcion as Descripcion, t.fecha_evento::date as FechaEvento, 
            t.hora_inicial::time as HoraInicial, t.hora_final::time as HoraFinal, 
            t.estado as Estado, t.usuario_crea as UsuarioCrea, 
            t.fecha_creacion::date as FechaCreacion, t.hora_crea::time as HoraCrea, 
            t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod, t.hora_mod::time as HoraMod,
            c.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            na.descripcion AS Nivel_Academico_Descripcion,
            tg.descripcion AS Tipo_Grado_Descripcion,
            tp.descripcion AS Tipo_Grupo_Descripcion
        FROM {Db.FullTable("actividades_eventos")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} c ON t.id_colegio = c.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("niveles_academicos")} na ON t.id_nivel_academico = na.id_nivel_academico
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tg ON t.id_tipo_grado = tg.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tp ON t.id_tipo_grupo = tp.id_tipo_grupo
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("actividades_eventos")} t";
        return await QueryPagedAsync<ActividadesEventosDto>(select,count,new{limit,offset});
    }

    public async Task<ActividadesEventosDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_actividad_evento as IdActividadesEventos,
            t.tipo_alcance as TipoAlcance, t.titulo_evento as TituloEvento,
            t.descripcion as Descripcion, t.fecha_evento::date as FechaEvento,
            t.hora_inicial::time as HoraInicial, t.hora_final::time as HoraFinal,
            t.estado as Estado, t.usuario_crea as UsuarioCrea,
            t.fecha_creacion::date as FechaCreacion, t.hora_crea::time as HoraCrea,
            t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod, t.hora_mod::time as HoraMod,
            c.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            na.descripcion AS Nivel_Academico_Descripcion,
            tg.descripcion AS Tipo_Grado_Descripcion,
            tp.descripcion AS Tipo_Grupo_Descripcion
        FROM {Db.FullTable("actividades_eventos")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} c ON t.id_colegio = c.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("niveles_academicos")} na ON t.id_nivel_academico = na.id_nivel_academico
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tg ON t.id_tipo_grado = tg.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tp ON t.id_tipo_grupo = tp.id_tipo_grupo
        WHERE t.id_actividad_evento = @id LIMIT 1";
        return await QuerySingleAsync<ActividadesEventosDto>(sql,new{id});
    }
}
