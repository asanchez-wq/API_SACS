using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class EvaluacionExternaEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/evaluacion_externa").WithTags("evaluacion_externa").RequireAuthorization();
        g.MapGet("", async (IEvaluacionExternaRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetEvaluacionesExternas").WithDescription("Obtiene evaluaciones externas con datos expandidos (periodo, grupo, herramienta didáctica, docente)");
        g.MapGet("/{id}", async (int id, IEvaluacionExternaRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetEvaluacionExternaById").WithDescription("Obtiene una evaluación externa por ID con datos expandidos");
    }
}

