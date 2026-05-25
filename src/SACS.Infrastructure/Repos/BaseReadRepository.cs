using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SACS.Infrastructure.Db;
namespace SACS.Infrastructure.Repos;
public abstract class BaseReadRepository
{
    protected readonly DbFactory Db;
    protected BaseReadRepository(DbFactory db) => Db = db;
    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param=null){ using var cn = Db.Create(); return await cn.QueryAsync<T>(sql,param); }
    protected async Task<T?> QuerySingleAsync<T>(string sql, object? param=null){ using var cn = Db.Create(); return await cn.QuerySingleOrDefaultAsync<T>(sql,param); }
    protected async Task<(IEnumerable<T> rows,long total)> QueryPagedAsync<T>(string selectSql,string countSql, object? param=null){
        using var cn = Db.Create(); using var multi = await cn.QueryMultipleAsync(selectSql+"; "+countSql,param);
        var rows = await multi.ReadAsync<T>(); var total = await multi.ReadSingleAsync<long>(); return (rows,total);
    }
}