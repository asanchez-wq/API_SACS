import re

# Definición de entidades con sus columnas (basado en el SQL original)
entities = {
    "Asistencia": [
        ("id_asistencia", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_matricula", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_periodo_x_horarios", "int?"),
        ("fecha_asistencia", "DateOnly?"),
        ("estado_asistencia", "string"),
        ("estado_proceso", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?"),
        ("tipo_procesado", "string")
    ],
    "Notas": [
        ("id_notas", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_evaluacion", "int?"),
        ("id_matricula", "int?"),
        ("calificacion_sumativa", "decimal"),
        ("calificacion_apreciativa", "decimal"),
        ("nota_sumativa", "decimal"),
        ("nota_apreciativa", "decimal"),
        ("observacion", "string"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?"),
        ("tipo_procesado", "string")
    ],
    "Matriculas": [
        ("id_matricula", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("codigo", "string"),
        ("id_alumno", "int?"),
        ("situacion", "string"),
        ("colegio_procedente", "string"),
        ("repitente", "int?"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?"),
        ("monto_matricula", "decimal?"),
        ("observaciones", "string"),
        ("id_validacion_externo", "Guid?")
    ],
    "Evaluacion": [
        ("id_evaluacion", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_curso", "int?"),
        ("id_docente", "int?"),
        ("id_tipo_evaluacion", "int?"),
        ("trimestre", "int?"),
        ("descripcion", "string"),
        ("calificacion", "decimal"),
        ("fecha_asignacion", "DateOnly?"),
        ("fecha_entrega", "DateOnly?"),
        ("tipo_nota", "string"),
        ("recurso", "byte[]"),
        ("nombre_recurso", "string"),
        ("tipo_recurso", "string"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?"),
        ("tipo_procesado", "string")
    ],
    "Periodos": [
        ("id_periodo", "int?"),
        ("id_colegio", "int?"),
        ("descripcion", "string"),
        ("fecha_inicio", "DateOnly?"),
        ("fecha_fin", "DateOnly?"),
        ("estado", "int?"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "PeriodosXGrupos": [
        ("id_periodo_x_grupo", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_grupo", "int?"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "PeriodosXHorarios": [
        ("id_periodo_x_horarios", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_curso", "int?"),
        ("id_docente", "int?"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "EvaluacionExterna": [
        ("id_evaluacion_externa", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_curso", "int?"),
        ("descripcion", "string"),
        ("puntaje", "decimal"),
        ("estado", "string"),
        ("tipo_procesado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "NotasExterna": [
        ("id_notas_externa", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_evaluacion_externa", "int?"),
        ("id_matricula", "int?"),
        ("puntaje", "decimal"),
        ("observacion", "string"),
        ("estado", "string"),
        ("tipo_procesado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "NotasManuales": [
        ("id_notas_manuales", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_curso", "int?"),
        ("id_matricula", "int?"),
        ("trimestre", "int?"),
        ("nota_trimestre", "decimal"),
        ("nota_conducta", "decimal"),
        ("nota_anual", "decimal"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "AsistenciasManuales": [
        ("id_asistencia_manuales", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_matricula", "int?"),
        ("trimestre", "int?"),
        ("total_ausencias", "int?"),
        ("total_justificaciones", "int?"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "ActividadesEventos": [
        ("id_actividades_eventos", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_curso", "int?"),
        ("descripcion", "string"),
        ("fecha_evento", "DateOnly?"),
        ("tipo_evento", "string"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "Citaciones": [
        ("id_citacion", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_matricula", "int?"),
        ("fecha_citacion", "DateOnly?"),
        ("motivo", "string"),
        ("observacion", "string"),
        ("estado", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "PlaneamientoDidactico": [
        ("id_plan_didactico", "int?"),
        ("id_colegio", "int?"),
        ("id_periodo", "int?"),
        ("id_periodo_x_grupo", "int?"),
        ("id_curso", "int?"),
        ("titulo", "string"),
        ("contenido", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?"),
        ("usuario_mod", "int?"),
        ("fecha_mod", "DateOnly?"),
        ("hora_mod", "TimeSpan?")
    ],
    "PlaneamientoDidacticoObjetivo": [
        ("id_plan_didactico_obj", "int?"),
        ("id_colegio", "int?"),
        ("id_plan_didactico", "int?"),
        ("objetivo", "string"),
        ("indicadores", "string"),
        ("evidencia", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?")
    ],
    "PlaneamientoDidacticoObjetivoCrit": [
        ("id_plan_didactico_obj_crit", "int?"),
        ("id_colegio", "int?"),
        ("id_plan_didactico", "int?"),
        ("id_plan_didactico_obj", "int?"),
        ("tipo_criterio", "string"),
        ("descripcion", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?")
    ],
    "PlaneamientoDidacticoObjetivoInst": [
        ("id_plan_didactico_obj_inst", "int?"),
        ("id_colegio", "int?"),
        ("id_plan_didactico", "int?"),
        ("id_plan_didactico_obj", "int?"),
        ("tipo_detalle", "string"),
        ("instrumento", "string"),
        ("usuario_crea", "int?"),
        ("fecha_creacion", "DateOnly?"),
        ("hora_crea", "TimeSpan?")
    ]
}

def snake_to_pascal(name):
    """Convierte snake_case a PascalCase"""
    return ''.join(word.capitalize() for word in name.split('_'))

def generate_entity(class_name, columns):
    """Genera el código C# para una entidad"""
    code = f"""using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SACS.Domain.Entities;

public class {class_name}
{{
"""
    
    for col_name, col_type in columns:
        prop_name = snake_to_pascal(col_name)
        code += f'    [Column("{col_name}")]\n'
        code += f'    public {col_type} {prop_name} {{ get; set; }}\n'
    
    code += "}\n"
    return code

# Generar todos los archivos
import os
entities_path = r"D:\Proyectos_Personales\sacs\src\SACS.Domain\Entities"

for class_name, columns in entities.items():
    file_path = os.path.join(entities_path, f"{class_name}.cs")
    content = generate_entity(class_name, columns)
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    print(f"✓ Generado: {class_name}.cs")

print(f"\n✓ {len(entities)} entidades generadas correctamente")
