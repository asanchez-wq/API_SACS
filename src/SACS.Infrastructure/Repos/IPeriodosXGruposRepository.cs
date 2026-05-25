using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPeriodosXGruposRepository
{
    Task<(IEnumerable<PeriodosXGrupos> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PeriodosXGrupos?> GetByIdAsync(int id);
    Task<(IEnumerable<PeriodosXGruposDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PeriodosXGruposDto?> GetByIdDtoAsync(int id);
}
