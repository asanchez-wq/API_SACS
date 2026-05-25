using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using SACS.Infrastructure.Repos;
namespace SACS.Api.Endpoints;
[Authorize]
public static class NotasManualesEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/notas-manuales").WithTags("notas-manuales").RequireAuthorization();
        g.MapGet("", async (INotasManualesRepository repo,int limit=50,int offset=0,string? orderBy=null,bool desc=false)=>{
            var (rows,total)=await repo.GetPagedDtoAsync(limit,offset,orderBy,desc); return Results.Ok(new{rows,total});
        }).WithName("GetNotasManuales").WithDescription("Obtiene notas manuales con datos expandidos (periodo, grupo, alumno, curso, trimestre)");
        g.MapGet("/{id}", async (int id, INotasManualesRepository repo)=>{
            var row=await repo.GetByIdDtoAsync(id); return row is null? Results.NotFound(): Results.Ok(row);
        }).WithName("GetNotaManualById").WithDescription("Obtiene una nota manual por ID con datos expandidos");
    }
}

