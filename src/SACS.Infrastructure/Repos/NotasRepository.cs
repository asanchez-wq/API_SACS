using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public class NotasRepository : BaseReadRepository, INotasRepository
{
    public NotasRepository(DbFactory db):base(db){}
    public async Task<(IEnumerable<Notas> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"id_notas":orderBy!;
        var dir=desc?"DESC":"ASC"; var table=Db.FullTable("notas");
        var select=$"SELECT t.id_notas, t.id_colegio, t.id_evaluacion, t.calificacion_sumativa, t.calificacion_apreciativa, t.nota_sumativa, t.nota_apreciativa, t.observacion, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.tipo_procesado FROM {table} t ORDER BY {Db.Quote(orderCol)} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {table} t";
        return await QueryPagedAsync<Notas>(select,count,new{limit,offset});
    }
    public async Task<Notas?> GetByIdAsync(int id){
        var table=Db.FullTable("notas");
        var sql=$"SELECT t.id_notas, t.id_colegio, t.id_evaluacion, t.calificacion_sumativa, t.calificacion_apreciativa, t.nota_sumativa, t.nota_apreciativa, t.observacion, t.estado, t.usuario_crea, t.fecha_creacion, t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.tipo_procesado FROM {table} t WHERE t.{Db.Quote("id_notas")} = @id LIMIT 1";
        return await QuerySingleAsync<Notas>(sql,new{id});
    }
    
    public async Task<(IEnumerable<NotasDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"t.id_notas":"t."+orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_notas AS IdNotas,
            t.calificacion_sumativa AS CalificacionSumativa, t.calificacion_apreciativa AS CalificacionApreciativa, 
            t.nota_sumativa AS NotaSumativa, t.nota_apreciativa AS NotaApreciativa,
            t.observacion AS Observacion, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod, t.tipo_procesado AS TipoProcesado,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            e.descripcion AS EvaluacionDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.codigo AS AlumnoCodigo,
            c.descripcion AS CursoDescripcion
        FROM {Db.FullTable("notas")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("evaluacion")} e ON t.id_evaluacion = e.id_evaluacion
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("evaluacion")} ev ON t.id_evaluacion = ev.id_evaluacion
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON ev.id_curso = c.id_curso
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("notas")} t";
        return await QueryPagedAsync<NotasDto>(select,count,new{limit,offset});
    }
    
    public async Task<NotasDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_notas AS IdNotas,
            t.calificacion_sumativa AS CalificacionSumativa, t.calificacion_apreciativa AS CalificacionApreciativa, 
            t.nota_sumativa AS NotaSumativa, t.nota_apreciativa AS NotaApreciativa,
            t.observacion AS Observacion, t.estado AS Estado, 
            t.usuario_crea AS UsuarioCrea, t.fecha_creacion::date AS FechaCreacion, t.hora_crea::time AS HoraCrea, 
            t.usuario_mod AS UsuarioMod, t.fecha_mod::date AS FechaMod, t.hora_mod::time AS HoraMod, t.tipo_procesado AS TipoProcesado,
            col.descripcion AS ColegioDescripcion,
            p.descripcion AS PeriodoDescripcion,
            e.descripcion AS EvaluacionDescripcion,
            a.nombres AS AlumnoNombres, a.apellidos AS AlumnoApellidos, a.codigo AS AlumnoCodigo,
            c.descripcion AS CursoDescripcion
        FROM {Db.FullTable("notas")} t
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("colegios")} col ON t.id_colegio = col.id_colegio
        LEFT JOIN {Db.FullTable("periodos")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("evaluacion")} e ON t.id_evaluacion = e.id_evaluacion
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.Quote("registro")}.{Db.Quote("alumnos")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("evaluacion")} ev ON t.id_evaluacion = ev.id_evaluacion
        LEFT JOIN {Db.Quote("configuracion")}.{Db.Quote("cursos")} c ON ev.id_curso = c.id_curso
        WHERE t.id_notas = @id LIMIT 1";
        return await QuerySingleAsync<NotasDto>(sql,new{id});
    }
}

