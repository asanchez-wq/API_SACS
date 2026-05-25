using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface ICitacionesRepository
{
    Task<(IEnumerable<Citaciones> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<Citaciones?> GetByIdAsync(int id);
    Task<(IEnumerable<CitacionesDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<CitacionesDto?> GetByIdDtoAsync(int id);
}
