using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface IEvaluacionExternaRepository
{
    Task<(IEnumerable<EvaluacionExterna> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<EvaluacionExterna?> GetByIdAsync(int id);
    Task<(IEnumerable<EvaluacionExternaDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<EvaluacionExternaDto?> GetByIdDtoAsync(int id);
}
