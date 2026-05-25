using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IActividadesEventosRepository
{
    Task<(IEnumerable<ActividadesEventos> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<ActividadesEventos?> GetByIdAsync(int id);
    Task<(IEnumerable<ActividadesEventosDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<ActividadesEventosDto?> GetByIdDtoAsync(int id);
}
