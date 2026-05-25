using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class AsistenciasManualesRepository : BaseReadRepository, IAsistenciasManualesRepository
{
    public AsistenciasManualesRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<AsistenciasManuales> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_asistencia_manual":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("asistencias_manuales");
        var select=$"SELECT t.id_asistencia_manual, t.id_colegio, t.id_trimestre, t.asistencia, t.tardanza, t.ausencia, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<AsistenciasManuales>(select,count,new{limit,offset});
    }
    public async Task<AsistenciasManuales?> GetByIdAsync(int id){
        var table=Db.FullTable("asistencias_manuales");
        var sql=$"SELECT t.id_asistencia_manual, t.id_colegio, t.id_trimestre, t.asistencia, t.tardanza, t.ausencia, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t WHERE t.{Db.Quote("id_asistencia_manual")} = @id LIMIT 1";
        return await QuerySingleAsync<AsistenciasManuales>(sql,new{id});
    }
    
    public async Task<IEnumerable<AsistenciasManuales>> GetAllAsync(){
        var table=Db.FullTable("asistencias_manuales");
        var sql=$"SELECT t.id_asistencia_manual, t.id_colegio, t.id_trimestre, t.asistencia, t.tardanza, t.ausencia, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t";
        return await QueryAsync<AsistenciasManuales>(sql);
    }

    public async Task<(IEnumerable<AsistenciasManualesDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_asistencia_manual":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_asistencia_manual, t.id_colegio, t.id_trimestre,
            t.asistencia, t.tardanza, t.ausencia, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea,
            t.usuario_mod, t.fecha_mod, t.hora_mod,
            p.descripcion AS periodo_descripcion,
            tg.descripcion AS grupo_descripcion,
            a.nombres AS alumno_nombres, a.apellidos AS alumno_apellidos, a.codigo AS alumno_codigo,
            c.descripcion AS curso_descripcion,
            tr.descripcion AS trimestre_descripcion
        FROM {Db.FullTable("asistencias_manuales")} t
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.id_trimestre = tr.id_trimestre
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("asistencias_manuales")} t";
        return await QueryPagedAsync<AsistenciasManualesDto>(select,count,new{limit,offset});
    }

    public async Task<AsistenciasManualesDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_asistencia_manual, t.id_colegio, t.id_trimestre,
            t.asistencia, t.tardanza, t.ausencia, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea,
            t.usuario_mod, t.fecha_mod, t.hora_mod,
            p.descripcion AS periodo_descripcion,
            tg.descripcion AS grupo_descripcion,
            a.nombres AS alumno_nombres, a.apellidos AS alumno_apellidos, a.codigo AS alumno_codigo,
            c.descripcion AS curso_descripcion,
            tr.descripcion AS trimestre_descripcion
        FROM {Db.FullTable("asistencias_manuales")} t
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.id_trimestre = tr.id_trimestre
        WHERE t.id_asistencia_manual = @id LIMIT 1";
        return await QuerySingleAsync<AsistenciasManualesDto>(sql,new{id});
    }
}

