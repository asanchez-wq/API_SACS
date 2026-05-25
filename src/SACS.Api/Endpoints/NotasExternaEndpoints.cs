using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class NotasExternaEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/notas-externa").WithTags("notas-externa").RequireAuthorization();
        g.MapGet("", async (INotasExternaRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetNotasExternas").WithDescription("Obtiene notas externas con datos expandidos (periodo, grupo, alumno, curso, trimestre)");
        g.MapGet("/{id}", async (int id, INotasExternaRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetNotaExternaById").WithDescription("Obtiene una nota externa por ID con datos expandidos");
    }
}

