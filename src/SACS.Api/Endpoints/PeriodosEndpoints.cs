using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class PeriodosEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/periodos").WithTags("periodos").RequireAuthorization();
        g.MapGet("", async (IPeriodosRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        })
        .WithName("GetPeriodos")
        .WithDescription("Obtiene periodos con descripción de colegio");
        
        g.MapGet("/{id}", async (int id, IPeriodosRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        })
        .WithName("GetPeriodoById")
        .WithDescription("Obtiene un periodo por ID con descripción de colegio");
    }
}

