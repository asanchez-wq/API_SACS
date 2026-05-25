using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPeriodosRepository
{
    Task<(IEnumerable<Periodos> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<Periodos?> GetByIdAsync(int id);
    Task<(IEnumerable<PeriodosDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PeriodosDto?> GetByIdDtoAsync(int id);
}
