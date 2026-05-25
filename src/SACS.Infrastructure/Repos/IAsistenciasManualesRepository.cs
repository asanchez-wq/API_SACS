using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IAsistenciasManualesRepository
{
    Task<(IEnumerable<AsistenciasManuales> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<AsistenciasManuales?> GetByIdAsync(int id);
    Task<IEnumerable<AsistenciasManuales>> GetAllAsync();
    Task<(IEnumerable<AsistenciasManualesDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<AsistenciasManualesDto?> GetByIdDtoAsync(int id);
}
