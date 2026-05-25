using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class PeriodosXHorariosEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/periodos-x-horarios").WithTags("periodos-x-horarios").RequireAuthorization();
        g.MapGet("", async (IPeriodosXHorariosRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetPeriodosXHorarios").WithDescription("Obtiene periodos por horarios con datos expandidos (periodo, grupo, curso, docente)");
        g.MapGet("/{id}", async (int id, IPeriodosXHorariosRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetPeriodoXHorarioById").WithDescription("Obtiene un periodo por horario por ID con datos expandidos");
    }
}

