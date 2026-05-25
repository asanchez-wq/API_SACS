using System.Collections.Generic;
using System.Threading.Tasks;
using SACS.Domain.Entities;
using SACS.Domain.DTOs;
namespace SACS.Infrastructure.Repos;
public interface INotasManualesRepository
{
    Task<(IEnumerable<NotasManuales> rows,long total)> GetPagedAsync(int limit,int offset,string? orderBy,bool desc);
    Task<NotasManuales?> GetByIdAsync(int id);
    Task<(IEnumerable<NotasManualesDto> rows,long total)> GetPagedDtoAsync(int limit,int offset,string? orderBy,bool desc);
    Task<NotasManualesDto?> GetByIdDtoAsync(int id);
}
