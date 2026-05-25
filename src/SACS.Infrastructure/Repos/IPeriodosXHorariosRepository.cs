using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IPeriodosXHorariosRepository
{
    Task<(IEnumerable<PeriodosXHorarios> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PeriodosXHorarios?> GetByIdAsync(int id);
    Task<(IEnumerable<PeriodosXHorariosDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<PeriodosXHorariosDto?> GetByIdDtoAsync(int id);
}
