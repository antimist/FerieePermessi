using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public async Task<DataSet> QueryAsync(FormattableString fromattableQuery)
        {

            var queryArguments = fromattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i< queryArguments.Length; i++)            
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = fromattableQuery.ToString();

            string strConn = "Data Source=Data/MyCourse.db";
            using(var conn = new SqliteConnection(strConn))
            {
                await conn.OpenAsync();
                using(var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameters);
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        
                        var dataSet = new DataSet();
                        //TODO: La rig qui sotto v rimossa quando la issue sarà risolta
                        //https://github.com/aspnet/EntityFrameworkCore/issues/14963
                        dataSet.EnforceConstraints = false;

                        do 
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);
                        }   while (!reader.IsClosed);

                        return dataSet;
                    }    
                }
            }
        }
    }
}