using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class PlaneamientoDidacticoEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/planeamiento_didactico").WithTags("planeamiento_didactico").RequireAuthorization();
        g.MapGet("", async (IPlaneamientoDidacticoRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetPlaneamientosDidacticos").WithDescription("Obtiene planeamientos didácticos con datos expandidos (periodo, nivel académico, tipo grado, curso, docente)");
        g.MapGet("/{id}", async (int id, IPlaneamientoDidacticoRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetPlaneamientoDidacticoById").WithDescription("Obtiene un planeamiento didáctico por ID con datos expandidos");
    }
}

