using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Musicalog.Infrastructure.Interfaces
{
    internal interface IDapper : IDisposable
    {
        Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
