using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class CitacionesRepository : BaseReadRepository, ICitacionesRepository
{
    public CitacionesRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<Citaciones> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_citaciones":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("citaciones");
        var select=$"SELECT t.id_citaciones, t.id_colegio, t.id_habitos_actitudes, t.id_trimestre, t.descripcion, t.nivel_urgencia, t.fecha_citacion, t.fecha_revision, t.hora_revision, t.id_apoderado_revision, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<Citaciones>(select,count,new{limit,offset});
    }
    public async Task<Citaciones?> GetByIdAsync(int id){
        var table=Db.FullTable("citaciones");
        var sql=$"SELECT t.id_citaciones, t.id_colegio, t.id_habitos_actitudes, t.id_trimestre, t.descripcion, t.nivel_urgencia, t.fecha_citacion, t.fecha_revision, t.hora_revision, t.id_apoderado_revision, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod FROM {table} t WHERE t.{Db.Quote("id_citaciones")} = @id LIMIT 1";
        return await QuerySingleAsync<Citaciones>(sql,new{id});
    }

    public async Task<(IEnumerable<CitacionesDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_citaciones":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_citaciones as IdCitaciones, t.id_colegio as IdColegio,
            t.id_habitos_actitudes as IdHabitosActitudes, t.id_trimestre as IdTrimestre,
            t.descripcion as Descripcion, t.nivel_urgencia as NivelUrgencia, 
            t.fecha_citacion::date as FechaCitacion, t.fecha_revision::date as FechaRevision, t.hora_revision::time as HoraRevision,
            t.id_apoderado_revision as IdApoderadoRevision, t.estado as Estado,
            t.usuario_crea as UsuarioCrea, t.fecha_creacion::date as FechaCreacion, t.hora_crea::time as HoraCrea,
            t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod, t.hora_mod::time as HoraMod,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos,
            ha.descripcion AS HabitoActitudDescripcion,
            tr.descripcion AS TrimestreDescripcion
        FROM {Db.FullTable("citaciones")} t
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("habitos_actitudes")} ha ON t.id_habitos_actitudes = ha.id_habitos_actitudes
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.id_trimestre = tr.id_trimestre
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("citaciones")} t";
        return await QueryPagedAsync<CitacionesDto>(select,count,new{limit,offset});
    }

    public async Task<CitacionesDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_citaciones as IdCitaciones, t.id_colegio as IdColegio,
            t.id_habitos_actitudes as IdHabitosActitudes, t.id_trimestre as IdTrimestre,
            t.descripcion as Descripcion, t.nivel_urgencia as NivelUrgencia,
            t.fecha_citacion::date as FechaCitacion, t.fecha_revision::date as FechaRevision, t.hora_revision::time as HoraRevision,
            t.id_apoderado_revision as IdApoderadoRevision, t.estado as Estado,
            t.usuario_crea as UsuarioCrea, t.fecha_creacion::date as FechaCreacion, t.hora_crea::time as HoraCrea,
            t.usuario_mod as UsuarioMod, t.fecha_mod::date as FechaMod, t.hora_mod::time as HoraMod,
            p.descripcion AS PeriodoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos,
            ha.descripcion AS HabitoActitudDescripcion,
            tr.descripcion AS TrimestreDescripcion
        FROM {Db.FullTable("citaciones")} t
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON t.id_docente = d.id_docente
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("habitos_actitudes")} ha ON t.id_habitos_actitudes = ha.id_habitos_actitudes
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("trimestres")} tr ON t.id_trimestre = tr.id_trimestre
        WHERE t.id_citaciones = @id LIMIT 1";
        return await QuerySingleAsync<CitacionesDto>(sql,new{id});
    }
}

