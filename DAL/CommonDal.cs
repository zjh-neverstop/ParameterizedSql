using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;

namespace DAL
{
    public class CommonDal
    {
        /// <summary>
        /// 根据sql语句获取结果，非参数化
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public static DataSet GetDataBySql(string sql)
        {

            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            using (DataAccess Access = new DataAccess(false))
            {
                return Access.Fill(command, ds);
            }
        }
        
        /// <summary>
        /// 根据参数化sql语句获取结果，select
        /// </summary>
        /// <param name="sql">参数化sql语句</param>
        /// <param name="paramList">参数对象列表</param>
        /// <returns></returns>
        public static DataSet GetDataByParameterizedSql(string sql, List<CustomSqlParam> paramList)
        {
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.Text;

            SqlParameterCollection sqlParams = command.Parameters;
            foreach (var sqlParam in paramList)
            {
                sqlParams.Add(sqlParam.Name, sqlParam.Type);
                sqlParams[sqlParam.Name].Value = sqlParam.Value;
            }

            DataSet ds = new DataSet();
            using (DataAccess Access = new DataAccess(false))
            {
                return Access.Fill(command, ds);
            }
        }
        
        /// <summary>
        /// 执行参数化sql语句，insert、update、delete
        /// </summary>
        /// <param name="sql">参数化sql语句</param>
        /// <param name="paramList">参数对象列表</param>
        /// <returns></returns>
        public static int ExecuteParameterizedSql(string sql, List<CustomSqlParam> paramList)
        {
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = CommandType.Text;

            SqlParameterCollection sqlParams = command.Parameters;
            foreach (var sqlParam in paramList)
            {
                sqlParams.Add(sqlParam.Name, sqlParam.Type);
                sqlParams[sqlParam.Name].Value = sqlParam.Value;
            }

            DataSet ds = new DataSet();
            using (DataAccess Access = new DataAccess(false))
            {
                return Access.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 执行参数化事务语句
        /// </summary>
        /// <param name="sqlDirectionary"></param>
        /// <returns></returns>
        public static int ExecuteTransSql(Dictionary<String,List<CustomSqlParam>> sqlDirectionary)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            int status = 1;
            try
            {
                DateTime dtnow = DateTime.Now;
                using (DataAccess Access = new DataAccess(false))
                {
                    conn = Access.Connection(true);
                    trans = conn.BeginTransaction();
                    SqlCommand command = new SqlCommand();
                    command.Transaction = trans;
                    command.CommandType = CommandType.Text;
                    foreach (var item in sqlDirectionary)
                    {
                        string sqlstr = item.Key;
                        command.CommandText = sqlstr;
                        SqlParameterCollection sqlParams = command.Parameters;
                        foreach (var sqlParam in item.Value)
                        {
                            sqlParams.Add(sqlParam.Name, sqlParam.Type);
                            sqlParams[sqlParam.Name].Value = sqlParam.Value;
                        }
                        int count = command.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
            }
            catch
            {
                trans.Rollback();
                status = 0;
            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
            return status;
        }
    }
}
