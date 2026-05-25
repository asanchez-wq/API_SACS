using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class PlaneamientoDidacticoObjetivoCritEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/planeamiento_didactico_objetivo_crit").WithTags("planeamiento_didactico_objetivo_crit").RequireAuthorization();
        g.MapGet("", async (IPlaneamientoDidacticoObjetivoCritRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        });
        g.MapGet("/{id}", async (int id, IPlaneamientoDidacticoObjetivoCritRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        });
    }
}

