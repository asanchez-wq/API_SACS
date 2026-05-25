using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IEvaluacionRepository
{
    Task<(IEnumerable<Evaluacion> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<Evaluacion?> GetByIdAsync(int id);
    Task<(IEnumerable<EvaluacionDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<EvaluacionDto?> GetByIdDtoAsync(int id);
}
