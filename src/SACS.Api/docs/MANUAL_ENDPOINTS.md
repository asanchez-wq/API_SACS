# Manual de Endpoints - SACS API

He añadido ejemplos JSON de respuesta en `src/SACS.Api/docs/examples/`. A continuación la relación de endpoints y el fichero ejemplo asociado:

- POST /auth/token → examples/auth_token.json
- GET /api/actividades_eventos → examples/actividades_eventos_list.json
- GET /api/actividades_eventos/{id} → examples/actividades_eventos_item.json
- GET /api/asistencia → examples/asistencia_list.json
- GET /api/asistencia/{id} → examples/asistencia_item.json
- GET /api/asistencias-manuales → examples/asistencias_manuales_list.json
- GET /api/asistencias-manuales/{id} → examples/asistencias_manuales_item.json
- GET /api/citaciones → examples/citaciones_list.json
- GET /api/citaciones/{id} → examples/citacion_item.json
- GET /api/evaluacion → examples/evaluacion_list.json
- GET /api/evaluacion/{id} → examples/evaluacion_item.json
- GET /api/evaluacion_externa → examples/evaluacion_externa_list.json
- GET /api/evaluacion_externa/{id} → examples/evaluacion_externa_item.json
- GET /api/matriculas → examples/matriculas_list.json
- GET /api/matriculas/{id} → examples/matricula_item.json
- GET /api/notas → examples/notas_list.json
- GET /api/notas/{id} → examples/nota_item.json
- GET /api/notas_externa → examples/notas_externa_list.json
- GET /api/notas_externa/{id} → examples/notas_externa_item.json
- GET /api/notas_manuales → examples/notas_manuales_list.json
- GET /api/notas_manuales/{id} → examples/notas_manuales_item.json
- GET /api/periodos → examples/periodos_list.json
- GET /api/periodos/{id} → examples/periodo_item.json
- GET /api/periodos_x_grupos → examples/periodos_x_grupos_list.json
- GET /api/periodos_x_grupos/{id} → examples/periodos_x_grupos_item.json
- GET /api/periodos_x_horarios → examples/periodos_x_horarios_list.json
- GET /api/periodos_x_horarios/{id} → examples/periodos_x_horarios_item.json
- GET /api/planeamiento_didactico → examples/planeamiento_didactico_list.json
- GET /api/planeamiento_didactico/{id} → examples/planeamiento_didactico_item.json
- GET /api/planeamiento_didactico_objetivo → examples/planeamiento_objetivo_list.json
- GET /api/planeamiento_didactico_objetivo/{id} → examples/planeamiento_objetivo_item.json
- GET /api/planeamiento_didactico_objetivo_crit → examples/planeamiento_objetivo_crit_list.json
- GET /api/planeamiento_didactico_objetivo_crit/{id} → examples/planeamiento_objetivo_crit_item.json
- GET /api/planeamiento_didactico_objetivo_inst → examples/planeamiento_objetivo_inst_list.json
- GET /api/planeamiento_didactico_objetivo_inst/{id} → examples/planeamiento_objetivo_inst_item.json

Ruta de los ejemplos: `src/SACS.Api/docs/examples/*.json`

Usos:
- Importar como mocks en Postman.
- Usar como fixtures para tests.
- Documentación y ejemplos en Swagger/manual.

Si quieres, puedo:
- Ajustar nombres de campos a los modelos reales (sube las clases o DTOs).
- Generar una colección Postman que enlace a estos ejemplos como "example responses".
- Convertir estos JSON en esquemas JSON Schema para validar respuestas en tests.