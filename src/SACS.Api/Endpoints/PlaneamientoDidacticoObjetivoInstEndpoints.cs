using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class PlaneamientoDidacticoObjetivoInstEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/planeamiento_didactico_objetivo_inst").WithTags("planeamiento_didactico_objetivo_inst").RequireAuthorization();
        g.MapGet("", async (IPlaneamientoDidacticoObjetivoInstRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        });
        g.MapGet("/{id}", async (int id, IPlaneamientoDidacticoObjetivoInstRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        });
    }
}

