using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IAsistenciaRepository
{
    Task<(IEnumerable<Asistencia> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<Asistencia?> GetByIdAsync(int id);
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<(IEnumerable<AsistenciaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<AsistenciaDto?> GetByIdDtoAsync(int id);
}
