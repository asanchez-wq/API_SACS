using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Http.Results;

namespace SACS.Api.Endpoints;

[Authorize]
public static class AsistenciaEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/asistencia")
            .WithTags("Asistencia")
            .RequireAuthorization();
            
        group.MapGet("/", async (IAsistenciaRepository repo, int limit=50, int offset=0, string? orderBy=null, bool desc=false) =>
        {
            var (rows,total) = await repo.GetPagedDtoAsync(limit,offset,orderBy,desc);
            return Ok(new{rows,total});
        }).WithName("GetAsistencias").WithDescription("Obtiene asistencias con datos expandidos (alumno, periodo, grupo, curso, docente)");

        group.MapGet("/{id}", async (int id, IAsistenciaRepository repo) =>
            await repo.GetByIdDtoAsync(id) is var item && item is not null
                ? Ok(item)
                : NotFound())
        .WithName("GetAsistenciaById").WithDescription("Obtiene una asistencia por ID con datos expandidos");
    }
}
