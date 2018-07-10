using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SWAG.Data
{
    public class SqlHelper
    {
        public SqlHelper()
        { }

        private Boolean ExecuteNonQuery(String sql, List<SqlParameter> paramList)
        {
            using (SqlConnection conn = new SqlConnection(AppDbContext.ConnectionString))
            {
                DbContextOptionsBuilder<AppDbContext> optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(AppDbContext.ConnectionString);

                AppDbContext context = new AppDbContext(optionsBuilder.Options);

                if (context.Database.Exists())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddRange(paramList.ToArray());
                        Int32 count = command.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }

            return false;
        }

        public Boolean InsertLog(EventEntity log)
        {
            if (log.Id == Guid.Empty)
            {
                log.Id = Guid.NewGuid();
            }

            if (!log.CreatedOn.HasValue)
            {
                log.CreatedOn = DateTime.UtcNow;
            }

            String command = @"BEGIN
                IF EXISTS (SELECT 1 FROM Information_Schema.Tables WHERE TABLE_NAME = 'Event')
                BEGIN
                    INSERT INTO [dbo].[Event] ({0}) VALUES ({1})
                END
            END";

            List<SqlParameter> paramList = new List<SqlParameter>();

            foreach (PropertyInfo property in log.GetType().GetProperties())
            {
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = property.Name,
                    Value = property.GetValue(log) ?? DBNull.Value,
                };

                paramList.Add(parameter);
            }

            command = String.Format(command,
                String.Join(", ", paramList.Select(p => $"[{p.ParameterName}]")), String.Join(", ", paramList.Select(p => $"@{p.ParameterName}")));

            return ExecuteNonQuery(command, paramList);
        }
    }
}
