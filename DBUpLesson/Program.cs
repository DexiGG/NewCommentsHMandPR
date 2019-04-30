using DbUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace DBUpLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringConfiguration = ConfigurationManager.ConnectionStrings["AppConnection"];
            var connectionString = connectionStringConfiguration.ConnectionString;
            #region Migration
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader =
                    DeployChanges.To
                        .SqlDatabase(connectionString)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                        .LogToConsole()
                        .Build();

            var result = upgrader.PerformUpgrade();
            if (!result.Successful) throw new Exception("Миграция не удалась");
#endregion

        }
    }
}
