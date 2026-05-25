using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPlaneamientoDidacticoObjetivoCritRepository
{
    Task<(IEnumerable<PlaneamientoDidacticoObjetivoCrit> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoObjetivoCrit?> GetByIdAsync(int id);
    Task<(IEnumerable<PlaneamientoDidacticoObjetivoCritDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoObjetivoCritDto?> GetByIdDtoAsync(int id);
}
