using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Http.Results;
namespace SACS.Api.Endpoints;
[Authorize]
public static class ActividadesEventosEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/actividades_eventos").WithTags("actividades_eventos").RequireAuthorization();
        g.MapGet("", async (IActividadesEventosRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetActividadesEventos").WithDescription("Obtiene actividades y eventos con datos expandidos (periodo, nivel académico, tipo grado, tipo grupo)");
        g.MapGet("/{id}", async (int id, IActividadesEventosRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetActividadEventoById").WithDescription("Obtiene una actividad o evento por ID con datos expandidos");
    }
}
