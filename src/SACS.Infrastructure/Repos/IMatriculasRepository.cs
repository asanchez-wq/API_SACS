using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IMatriculasRepository
{
    Task<(IEnumerable<Matriculas> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<Matriculas?> GetByIdAsync(int id);
    Task<(IEnumerable<MatriculasDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<MatriculasDto?> GetByIdDtoAsync(int id);
    Task<MatriculasDto?> GetByIdentificacionDtoAsync(string identificacion);
}
