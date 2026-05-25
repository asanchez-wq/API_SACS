using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SACS.Infrastructure.Repos;
using static Microsoft.AspNetCore.Http.Results;

namespace SACS.Api.Endpoints;

public static class GeneratedEndpoints
{
    public static void Map(WebApplication app)
    {
        AsistenciaEndpoints.Map(app);
        AsistenciasManualesEndpoints.Map(app);
        ActividadesEventosEndpoints.Map(app);
        CitacionesEndpoints.Map(app);
        EvaluacionEndpoints.Map(app);
        EvaluacionExternaEndpoints.Map(app);
        MatriculasEndpoints.Map(app);
        NotasEndpoints.Map(app);
        NotasExternaEndpoints.Map(app);
        NotasManualesEndpoints.Map(app);
        PeriodosEndpoints.Map(app);
        PeriodosXGruposEndpoints.Map(app);
        PeriodosXHorariosEndpoints.Map(app);
        PlaneamientoDidacticoEndpoints.Map(app);
        PlaneamientoDidacticoObjetivoCritEndpoints.Map(app);
        PlaneamientoDidacticoObjetivoEndpoints.Map(app);
        PlaneamientoDidacticoObjetivoInstEndpoints.Map(app);
    }
}