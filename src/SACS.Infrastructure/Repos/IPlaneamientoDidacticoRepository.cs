using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPlaneamientoDidacticoRepository
{
    Task<(IEnumerable<PlaneamientoDidactico> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidactico?> GetByIdAsync(int id);
    Task<(IEnumerable<PlaneamientoDidacticoDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PlaneamientoDidacticoDto?> GetByIdDtoAsync(int id);
}
