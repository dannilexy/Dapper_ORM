 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperORM2.Models
{
    public static class DapperORM
    {
        private static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = DapperDb;Integrated Security=True;";
        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using(SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                sqlconn.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }


        //DapperORM.ExecuteReturnScalar<int>(_,_);
        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
            return  (T)Convert.ChangeType  ( sqlconn.ExecuteScalar (procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }


        //DapperORM.ReturnList<EmployeeModel>
        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                     return   sqlconn.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
