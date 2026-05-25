using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class EvaluacionEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/evaluacion").WithTags("evaluacion").RequireAuthorization();
        g.MapGet("", async (IEvaluacionRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetEvaluaciones").WithDescription("Obtiene evaluaciones con datos expandidos (grupo, curso, docente, tipo evaluación, periodo)");
        g.MapGet("/{id}", async (int id, IEvaluacionRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetEvaluacionById").WithDescription("Obtiene una evaluación por ID con datos expandidos");
    }
}

