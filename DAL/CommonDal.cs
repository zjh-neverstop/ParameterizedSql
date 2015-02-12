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
        /// 根据参数化sql语句获取结果
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
    }
}
