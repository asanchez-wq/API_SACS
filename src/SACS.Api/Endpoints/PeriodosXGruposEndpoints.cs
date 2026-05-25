using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class PeriodosXGruposEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/periodos-x-grupos").WithTags("periodos-x-grupos").RequireAuthorization();
        g.MapGet("", async (IPeriodosXGruposRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetPeriodosXGrupos").WithDescription("Obtiene periodos por grupos con datos expandidos (periodo, nivel académico, tipo grado, tipo grupo, docente)");
        g.MapGet("/{id}", async (int id, IPeriodosXGruposRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetPeriodoXGrupoById").WithDescription("Obtiene un periodo por grupo por ID con datos expandidos");
    }
}

