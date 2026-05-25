using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class MatriculasEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/matriculas").WithTags("matriculas").RequireAuthorization();
        g.MapGet("", async (IMatriculasRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetMatriculas").WithDescription("Obtiene matrículas con datos expandidos (alumno, grupo, periodo)");
        g.MapGet("/{id}", async (int id, IMatriculasRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetMatriculaById").WithDescription("Obtiene una matrícula por ID con datos expandidos");
        
        g.MapGet("/identificacion/{identificacion}", async (string identificacion, IMatriculasRepository repo)=>{
            var row=await repo.GetByIdentificacionDtoAsync(identificacion); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetMatriculaByIdentificacion").WithDescription("Obtiene una matrícula por identificación del alumno con datos expandidos");
    }
}

