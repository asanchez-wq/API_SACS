using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface INotasRepository
{
    Task<(IEnumerable<Notas> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<Notas?> GetByIdAsync(int id);
    Task<(IEnumerable<NotasDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<NotasDto?> GetByIdDtoAsync(int id);
}
