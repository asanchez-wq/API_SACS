using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class NotasEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/notas").WithTags("notas").RequireAuthorization();
        g.MapGet("", async (INotasRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetNotasPaged").WithDescription("Obtiene notas con información expandida (alumno, evaluación, curso, periodo)");
        g.MapGet("/{id}", async (int id, INotasRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetNotasById").WithDescription("Obtiene una nota por ID con información expandida");
    }
}

