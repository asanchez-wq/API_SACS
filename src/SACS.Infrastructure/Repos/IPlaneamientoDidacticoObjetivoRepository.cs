using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPlaneamientoDidacticoObjetivoRepository
{
    Task<(IEnumerable<PlaneamientoDidacticoObjetivo> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoObjetivo?> GetByIdAsync(int id);
    Task<(IEnumerable<PlaneamientoDidacticoObjetivoDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoObjetivoDto?> GetByIdDtoAsync(int id);
}
