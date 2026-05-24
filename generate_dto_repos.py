# Script para generar implementaciones DTO en repositorios

repositories_dto_implementations = {
    "MatriculasRepository": {
        "file": "D:\\Proyectos_Personales\\sacs\\src\\SACS.Infrastructure\\Repos\\MatriculasRepository.cs",
        "dto_methods": '''
    public async Task<(IEnumerable<MatriculasDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"t.id_matricula":"t."+orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_matricula, t.id_colegio, t.id_periodo, t.id_periodo_x_grupo, t.codigo, t.id_alumno,
            t.situacion, t.colegio_procedente, t.repitente, t.estado, t.usuario_crea, t.fecha_creacion,
            t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.monto_matricula, t.observaciones, t.id_validacion_externo,
            p.descripcion AS periodo_descripcion,
            g.id_grupo, g.descripcion AS grupo_descripcion,
            a.nombres AS alumno_nombres, a.apellidos AS alumno_apellidos, a.identificacion AS alumno_identificacion
        FROM {Db.FullTable("matriculas")} t
        LEFT JOIN {Db.FullTable("periodos", "configuracion")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.FullTable("grados_x_grupos", "configuracion")} g ON pxg.id_grupo = g.id_grupo
        LEFT JOIN {Db.FullTable("alumnos", "registro")} a ON t.id_alumno = a.id_alumno
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("matriculas")} t";
        return await QueryPagedAsync<MatriculasDto>(select,count,new{limit,offset});
    }
    
    public async Task<MatriculasDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_matricula, t.id_colegio, t.id_periodo, t.id_periodo_x_grupo, t.codigo, t.id_alumno,
            t.situacion, t.colegio_procedente, t.repitente, t.estado, t.usuario_crea, t.fecha_creacion,
            t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.monto_matricula, t.observaciones, t.id_validacion_externo,
            p.descripcion AS periodo_descripcion,
            g.id_grupo, g.descripcion AS grupo_descripcion,
            a.nombres AS alumno_nombres, a.apellidos AS alumno_apellidos, a.identificacion AS alumno_identificacion
        FROM {Db.FullTable("matriculas")} t
        LEFT JOIN {Db.FullTable("periodos", "configuracion")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.FullTable("grados_x_grupos", "configuracion")} g ON pxg.id_grupo = g.id_grupo
        LEFT JOIN {Db.FullTable("alumnos", "registro")} a ON t.id_alumno = a.id_alumno
        WHERE t.id_matricula = @id LIMIT 1";
        return await QuerySingleAsync<MatriculasDto>(sql,new{id});
    }'''
    },
    "AsistenciaRepository": {
        "file": "D:\\Proyectos_Personales\\sacs\\src\\SACS.Infrastructure\\Repos\\AsistenciaRepository.cs",
        "dto_methods": '''
    public async Task<(IEnumerable<AsistenciaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"t.id_asistencia":"t."+orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_asistencia, t.id_colegio, t.id_periodo, t.id_matricula, t.id_periodo_x_grupo, t.id_periodo_x_horarios,
            t.fecha_asistencia, t.estado_asistencia, t.estado_proceso, t.usuario_crea, t.fecha_creacion,
            t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.tipo_procesado,
            p.descripcion AS periodo_descripcion,
            a.nombres AS alumno_nombres, a.apellidos AS alumno_apellidos, a.codigo AS alumno_codigo,
            g.id_grupo, g.descripcion AS grupo_descripcion,
            c.id_curso, c.descripcion AS curso_descripcion,
            d.id_docente, d.nombres AS docente_nombres, d.apellidos AS docente_apellidos
        FROM {Db.FullTable("asistencia")} t
        LEFT JOIN {Db.FullTable("periodos", "configuracion")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.FullTable("alumnos", "registro")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.FullTable("grados_x_grupos", "configuracion")} g ON pxg.id_grupo = g.id_grupo
        LEFT JOIN {Db.FullTable("periodos_x_horarios")} pxh ON t.id_periodo_x_horarios = pxh.id_periodo_x_horarios
        LEFT JOIN {Db.FullTable("cursos", "configuracion")} c ON pxh.id_curso = c.id_curso
        LEFT JOIN {Db.FullTable("docentes", "registro")} d ON pxh.id_docente = d.id_docente
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("asistencia")} t";
        return await QueryPagedAsync<AsistenciaDto>(select,count,new{limit,offset});
    }
    
    public async Task<AsistenciaDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_asistencia, t.id_colegio, t.id_periodo, t.id_matricula, t.id_periodo_x_grupo, t.id_periodo_x_horarios,
            t.fecha_asistencia, t.estado_asistencia, t.estado_proceso, t.usuario_crea, t.fecha_creacion,
            t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod, t.tipo_procesado,
            p.descripcion AS periodo_descripcion,
            a.nombres AS alumno_nombres, a.apellidos AS alumno_apellidos, a.codigo AS alumno_codigo,
            g.id_grupo, g.descripcion AS grupo_descripcion,
            c.id_curso, c.descripcion AS curso_descripcion,
            d.id_docente, d.nombres AS docente_nombres, d.apellidos AS docente_apellidos
        FROM {Db.FullTable("asistencia")} t
        LEFT JOIN {Db.FullTable("periodos", "configuracion")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("matriculas")} m ON t.id_matricula = m.id_matricula
        LEFT JOIN {Db.FullTable("alumnos", "registro")} a ON m.id_alumno = a.id_alumno
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.FullTable("grados_x_grupos", "configuracion")} g ON pxg.id_grupo = g.id_grupo
        LEFT JOIN {Db.FullTable("periodos_x_horarios")} pxh ON t.id_periodo_x_horarios = pxh.id_periodo_x_horarios
        LEFT JOIN {Db.FullTable("cursos", "configuracion")} c ON pxh.id_curso = c.id_curso
        LEFT JOIN {Db.FullTable("docentes", "registro")} d ON pxh.id_docente = d.id_docente
        WHERE t.id_asistencia = @id LIMIT 1";
        return await QuerySingleAsync<AsistenciaDto>(sql,new{id});
    }'''
    },
    "EvaluacionRepository": {
        "file": "D:\\Proyectos_Personales\\sacs\\src\\SACS.Infrastructure\\Repos\\EvaluacionRepository.cs",
        "dto_methods": '''
    public async Task<(IEnumerable<EvaluacionDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc){
        var orderCol=string.IsNullOrWhiteSpace(orderBy)?"t.id_evaluacion":"t."+orderBy!;
        var dir=desc?"DESC":"ASC";
        var select=$@"SELECT 
            t.id_evaluacion, t.id_colegio, t.id_periodo, t.id_periodo_x_grupo, t.id_curso, t.id_docente,
            t.id_tipo_evaluacion, t.trimestre, t.descripcion, t.calificacion, t.fecha_asignacion, t.fecha_entrega,
            t.tipo_nota, t.recurso, t.nombre_recurso, t.tipo_recurso, t.estado, t.usuario_crea, t.fecha_creacion,
            t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod,
            p.descripcion AS periodo_descripcion,
            g.id_grupo, g.descripcion AS grupo_descripcion,
            c.descripcion AS curso_descripcion,
            d.nombres AS docente_nombres, d.apellidos AS docente_apellidos,
            te.descripcion AS tipo_evaluacion_descripcion
        FROM {Db.FullTable("evaluacion")} t
        LEFT JOIN {Db.FullTable("periodos", "configuracion")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.FullTable("grados_x_grupos", "configuracion")} g ON pxg.id_grupo = g.id_grupo
        LEFT JOIN {Db.FullTable("cursos", "configuracion")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.FullTable("docentes", "registro")} d ON t.id_docente = d.id_docente
        LEFT JOIN {Db.FullTable("tipo_evaluacion", "configuracion")} te ON t.id_tipo_evaluacion = te.id_tipo_evaluacion
        ORDER BY {orderCol} {dir} LIMIT @limit OFFSET @offset";
        var count=$"SELECT COUNT(*) FROM {Db.FullTable("evaluacion")} t";
        return await QueryPagedAsync<EvaluacionDto>(select,count,new{limit,offset});
    }
    
    public async Task<EvaluacionDto?> GetByIdDtoAsync(int id){
        var sql=$@"SELECT 
            t.id_evaluacion, t.id_colegio, t.id_periodo, t.id_periodo_x_grupo, t.id_curso, t.id_docente,
            t.id_tipo_evaluacion, t.trimestre, t.descripcion, t.calificacion, t.fecha_asignacion, t.fecha_entrega,
            t.tipo_nota, t.recurso, t.nombre_recurso, t.tipo_recurso, t.estado, t.usuario_crea, t.fecha_creacion,
            t.hora_crea, t.usuario_mod, t.fecha_mod, t.hora_mod,
            p.descripcion AS periodo_descripcion,
            g.id_grupo, g.descripcion AS grupo_descripcion,
            c.descripcion AS curso_descripcion,
            d.nombres AS docente_nombres, d.apellidos AS docente_apellidos,
            te.descripcion AS tipo_evaluacion_descripcion
        FROM {Db.FullTable("evaluacion")} t
        LEFT JOIN {Db.FullTable("periodos", "configuracion")} p ON t.id_periodo = p.id_periodo
        LEFT JOIN {Db.FullTable("periodos_x_grupos")} pxg ON t.id_periodo_x_grupo = pxg.id_periodo_x_grupo
        LEFT JOIN {Db.FullTable("grados_x_grupos", "configuracion")} g ON pxg.id_grupo = g.id_grupo
        LEFT JOIN {Db.FullTable("cursos", "configuracion")} c ON t.id_curso = c.id_curso
        LEFT JOIN {Db.FullTable("docentes", "registro")} d ON t.id_docente = d.id_docente
        LEFT JOIN {Db.FullTable("tipo_evaluacion", "configuracion")} te ON t.id_tipo_evaluacion = te.id_tipo_evaluacion
        WHERE t.id_evaluacion = @id LIMIT 1";
        return await QuerySingleAsync<EvaluacionDto>(sql,new{id});
    }'''
    }
}

print("Implementaciones DTO generadas para copiar manualmente a los repositorios")
