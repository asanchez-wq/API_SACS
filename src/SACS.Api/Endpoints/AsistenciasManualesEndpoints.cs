using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Http.Results;

namespace SACS.Api.Endpoints;

[Authorize]
public static class AsistenciasManualesEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/asistencias-manuales")
            .WithTags("AsistenciasManuales")
            .RequireAuthorization();
            
        group.MapGet("/", async (IAsistenciasManualesRepository repo, int limit=50, int offset=0, string? orderBy=null, bool desc=false) =>
        {
            var (rows,total) = await repo.GetPagedDtoAsync(limit,offset,orderBy,desc);
            return Ok(new{rows,total});
        }).WithName("GetAsistenciasManuales").WithDescription("Obtiene asistencias manuales con datos expandidos (periodo, grupo, alumno, curso, trimestre)");

        group.MapGet("/{id}", async (int id, IAsistenciasManualesRepository repo) =>
            await repo.GetByIdDtoAsync(id) is var item && item is not null
                ? Ok(item)
                : NotFound())
        .WithName("GetAsistenciaManualById").WithDescription("Obtiene una asistencia manual por ID con datos expandidos");
    }
}
