using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPlaneamientoDidacticoObjetivoInstRepository
{
    Task<(IEnumerable<PlaneamientoDidacticoObjetivoInst> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoObjetivoInst?> GetByIdAsync(int id);
    Task<(IEnumerable<PlaneamientoDidacticoObjetivoInstDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoObjetivoInstDto?> GetByIdDtoAsync(int id);
}
