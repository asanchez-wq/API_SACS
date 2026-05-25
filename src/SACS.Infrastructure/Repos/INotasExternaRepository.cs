using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface INotasExternaRepository
{
    Task<(IEnumerable<NotasExterna> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<NotasExterna?> GetByIdAsync(int id);
    Task<(IEnumerable<NotasExternaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<NotasExternaDto?> GetByIdDtoAsync(int id);
}
