using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class AsistenciaRepository : BaseReadRepository, IAsistenciaRepository
{
    public AsistenciaRepository(DbFactory db):base(db){}

    public async Task<(IEnumerable<Asistencia> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_asistencia":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$"SELECT * FROM {Db.FullTable("asistencia")} ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("asistencia")}";
        return await QueryPagedAsync<Asistencia>(select,count,new{limit,offset});
    }

    public async Task<Asistencia?> GetByIdAsync(int id){
        var sql=$"SELECT * FROM {Db.FullTable("asistencia")} WHERE id_asistencia=@id LIMIT 1";
        return await QuerySingleAsync<Asistencia>(sql,new{id});
    }

    public async Task<IEnumerable<Asistencia>> GetAllAsync(){
        var sql=$"SELECT * FROM {Db.FullTable("asistencia")}";
        return await QueryAsync<Asistencia>(sql);
    }

    public async Task<(IEnumerable<AsistenciaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_asistencia":orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_asistencia AS IdAsistencia,
            t.fecha_asistencia AS FechaAsistencia, t.estado_asistencia AS EstadoAsistencia, t.estado_proceso AS EstadoProceso, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.codigo AS AlumnoCodigo,
            tg.descripcion AS GrupoDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("asistencia")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("periodos_x_horarios")} pxh ON t.id_periodo_x_horarios = pxh.id_periodo_x_horarios
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON pxh.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON pxh.id_docente = d.id_docente
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("asistencia")} t";
        return await QueryPagedAsync<AsistenciaDto>(select,count,new{limit,offset});
    }

    public async Task<AsistenciaDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_asistencia AS IdAsistencia,
            t.fecha_asistencia AS FechaAsistencia, t.estado_asistencia AS EstadoAsistencia, t.estado_proceso AS EstadoProceso, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.codigo AS AlumnoCodigo,
            tg.descripcion AS GrupoDescripcion,
            c.descripcion AS CursoDescripcion,
            d.nombres AS DocenteNombres, d.apellidos AS DocenteApellidos
        FROM {Db.FullTable("asistencia")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.FullTable("periodos_x_horarios")} pxh ON t.id_periodo_x_horarios = pxh.id_periodo_x_horarios
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON pxh.id_curso = c.id_curso
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("docentes")} d ON pxh.id_docente = d.id_docente
        WHERE t.id_asistencia = @id LIMIT 1";
        return await QuerySingleAsync<AsistenciaDto>(sql,new{id});
    }
}
