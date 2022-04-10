using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCourse.Models.Exceptions;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        private readonly ILogger<SqliteDatabaseAccessor> logger;
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions;


        public SqliteDatabaseAccessor(ILogger<SqliteDatabaseAccessor> logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions)
        {
            this.logger = logger;
            this.connectionStringOptions = connectionStringOptions;
        }

        public async Task<DataSet> QueryAsync(FormattableString fromattableQuery)
        {
            logger.LogInformation(fromattableQuery.Format, fromattableQuery.GetArguments());

            string strConn = connectionStringOptions.CurrentValue.Default;
            using SqliteConnection conn = await GetOpenedConnection(strConn);
            using SqliteCommand cmd = GetCommand(fromattableQuery, conn);

            //Inviamo la query al database e otteniamo un SQLiteDataReader
            //per leggere i risultati
            using var reader = await cmd.ExecuteReaderAsync();
            var dataSet = new DataSet();

            //Creiamo tanti DataTable per quante sono le tabelle
            //di risultati da trovare dal SqliteDataReader
            do
            {
                var dataTable = new DataTable();
                dataSet.Tables.Add(dataTable);
                dataTable.Load(reader);
            } while (!reader.IsClosed);

            return dataSet;
        }

        private static SqliteCommand GetCommand(FormattableString formattableQuery, SqliteConnection conn)
        {
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                if (queryArguments[i] is Sql)
                {
                    continue;
                }
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i] ?? DBNull.Value);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            System.Console.WriteLine(formattableQuery.Format, formattableQuery.GetArguments());
            var cmd = new SqliteCommand(query, conn);

            cmd.Parameters.AddRange(sqliteParameters);
            return cmd;
        }

        private static async Task<SqliteConnection> GetOpenedConnection(string strConn)
        {
            // string strConn = connectionStringOptions.CurrentValue.Default;
            var conn = new SqliteConnection(strConn);

            await conn.OpenAsync();
            return conn;
        }

        public async Task<int> CommandAsync(FormattableString formattableComand)
        {
            try
            {
                string strConn = connectionStringOptions.CurrentValue.Default;
                using SqliteConnection conn = await GetOpenedConnection(strConn);
                using SqliteCommand cmd = GetCommand(formattableComand, conn);
                int affectedRow = await cmd.ExecuteNonQueryAsync();
                return affectedRow;
            }
            catch (SqliteException exc) when (exc.SqliteExtendedErrorCode == 19)
            { 
                throw new ConstraintViolationException(exc);
            }
        }
        public async Task<T> QueryScalarAsync<T>(FormattableString formattableQuery)
        {
            string strConn = connectionStringOptions.CurrentValue.Default;
            using SqliteConnection conn = await GetOpenedConnection(strConn);
            using SqliteCommand cmd = GetCommand(formattableQuery, conn);
            object result = await cmd.ExecuteScalarAsync();
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}