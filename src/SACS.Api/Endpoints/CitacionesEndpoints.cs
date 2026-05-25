using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class CitacionesEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/citaciones").WithTags("citaciones").RequireAuthorization();
        g.MapGet("", async (ICitacionesRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetCitaciones").WithDescription("Obtiene citaciones con datos expandidos (periodo, grupo, alumno, docente, hábito/actitud, trimestre)");
        g.MapGet("/{id}", async (int id, ICitacionesRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetCitacionById").WithDescription("Obtiene una citación por ID con datos expandidos");
    }
}

