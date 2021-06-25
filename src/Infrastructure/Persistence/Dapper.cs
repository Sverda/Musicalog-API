using Dapper;
using Microsoft.Extensions.Configuration;
using Musicalog.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Musicalog.Infrastructure.Persistence
{
    internal class Dapper : IDapper
    {
        private readonly IConfiguration _config;
        private readonly string Connectionstring = "DefaultConnection";

        public Dapper(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {
        }

        public async Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).FirstOrDefault();
        }

        public async Task<IEnumerable<TOne>> GetAllWithOneToMany<TOne, TMany>(
            string query,
            Action<TOne, TMany> setProperty
        )
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var result = await db.QueryAsync<TOne, TMany, TOne>(query, (one, many) =>
            {
                setProperty(one, many);
                return one;
            });

            return result;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.QueryAsync<T>(sp, parms, commandType: commandType);
        }

        public async Task<T> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                using IDbTransaction transaction = db.BeginTransaction();
                try
                {
                    result = (await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: transaction))
                        .FirstOrDefault();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return result;
        }

        public async Task Update(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                using IDbTransaction transaction = db.BeginTransaction();
                try
                {
                    await db.ExecuteAsync(sp, parms, commandType: commandType, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
        }
    }
}
