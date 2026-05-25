using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class MatriculasRepository : BaseReadRepository, IMatriculasRepository
{
    public MatriculasRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<Matriculas> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_matricula":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("matriculas");
        var select=$"SELECT t.id_colegio, t.codigo, t.id_alumno, t.situacion, t.colegio_procedente, t.repitente, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.monto_matricula, t.observaciones, t.id_validacion_externo FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<Matriculas>(select,count,new{limit,offset});
    }
    public async Task<Matriculas?> GetByIdAsync(int id){
        var table=Db.FullTable("matriculas");
        var sql=$"SELECT t.id_colegio, t.codigo, t.id_alumno, t.situacion, t.colegio_procedente, t.repitente, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.monto_matricula, t.observaciones, t.id_validacion_externo FROM {table} t WHERE t.{Db.Quote("id_matricula")} = @id LIMIT 1";
        return await QuerySingleAsync<Matriculas>(sql,new{id});
    }
    
    public async Task<(IEnumerable<MatriculasDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"t.id_matricula":"t."+orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_matricula AS IdMatricula,
            t.codigo AS Codigo,
            t.situacion AS Situacion, t.colegio_procedente AS ColegioProcedente, t.repitente AS Repitente, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod, 
            t.monto_matricula AS MontoMatricula, t.observaciones AS Observaciones, t.id_validacion_externo AS IdValidacionExterno,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tgr.descripcion AS TipoGradoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.identificacion AS AlumnoIdentificacion
        FROM {Db.FullTable("matriculas")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tgr ON pxg.id_tipo_grado = tgr.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON t.id_alumno = a.id_alumno
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("matriculas")} t";
        return await QueryPagedAsync<MatriculasDto>(select,count,new{limit,offset});
    }
    
    public async Task<MatriculasDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_matricula AS IdMatricula,
            t.codigo AS Codigo,
            t.situacion AS Situacion, t.colegio_procedente AS ColegioProcedente, t.repitente AS Repitente, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod, 
            t.monto_matricula AS MontoMatricula, t.observaciones AS Observaciones, t.id_validacion_externo AS IdValidacionExterno,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tgr.descripcion AS TipoGradoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.identificacion AS AlumnoIdentificacion
        FROM {Db.FullTable("matriculas")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tgr ON pxg.id_tipo_grado = tgr.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON t.id_alumno = a.id_alumno
        WHERE t.id_matricula = @id LIMIT 1";
        return await QuerySingleAsync<MatriculasDto>(sql,new{id});
    }

    public async Task<MatriculasDto?> GetByIdentificacionDtoAsync(string identificacion){
        var sql=$@"SELECT 
            t.id_matricula AS IdMatricula,
            t.codigo AS Codigo,
            t.situacion AS Situacion, t.colegio_procedente AS ColegioProcedente, t.repitente AS Repitente, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea,
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod, 
            t.monto_matricula AS MontoMatricula, t.observaciones AS Observaciones, t.id_validacion_externo AS IdValidacionExterno,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            tgr.descripcion AS TipoGradoDescripcion,
            tg.descripcion AS GrupoDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.identificacion AS AlumnoIdentificacion
        FROM {Db.FullTable("matriculas")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grados")} tgr ON pxg.id_tipo_grado = tgr.id_tipo_grado
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("tipo_grupos")} tg ON pxg.id_tipo_grupo = tg.id_tipo_grupo
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON t.id_alumno = a.id_alumno
        WHERE a.identificacion = @identificacion LIMIT 1";
        return await QuerySingleAsync<MatriculasDto>(sql,new{identificacion});
    }
}
