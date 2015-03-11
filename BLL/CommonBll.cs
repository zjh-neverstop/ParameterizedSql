using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;
using DAL;

namespace BLL
{
    public class CommonBll
    {
        public static string Information;

        /// <summary>
        /// 根据sql语句获取结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public static DataSet GetDataBySql(string sql)
        {
            try
            {
                return CommonDal.GetDataBySql(sql);
            }
            catch (ConnectionException Ex)
            {
                Information = Ex.Message;
                return null;
            }
            catch (DataAccessExcption Ex)
            {
                Information = Ex.Message;
                return null;
            }
            catch (Exception Ex)
            {
                Errors.WriteLog(Ex.Message, Ex);
                Information = Ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据参数化sql语句获取结果
        /// </summary>
        /// <param name="sql">参数化sql语句</param>
        /// <param name="paramList">参数对象列表</param>
        /// <returns></returns>
        public static DataSet GetDataByParameterizedSql(string sql, List<CustomSqlParam> paramList)
        {
            try
            {
                return CommonDal.GetDataByParameterizedSql(sql,paramList);
            }
            catch (ConnectionException Ex)
            {
                Information = Ex.Message;
                return null;
            }
            catch (DataAccessExcption Ex)
            {
                Information = Ex.Message;
                return null;
            }
            catch (Exception Ex)
            {
                Errors.WriteLog(Ex.Message, Ex);
                Information = Ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 执行参数化sql语句
        /// </summary>
        /// <param name="sql">参数化sql语句</param>
        /// <param name="paramList">参数对象列表</param>
        /// <returns></returns>
        public static bool ExecuteParameterizedSql(string sql, List<CustomSqlParam> paramList)
        {
            try
            {
                int affectcount = CommonDal.ExecuteParameterizedSql(sql, paramList);
                Information = "操作成功,共影响[" + affectcount.ToString() + "]条记录!";
                return true;
            }
            catch (ConnectionException Ex)
            {
                Information = Ex.Message;
                return false;
            }
            catch (DataAccessExcption Ex)
            {
                Information = Ex.Message;
                return false;
            }
            catch (Exception Ex)
            {
                Errors.WriteLog(Ex.Message, Ex);
                Information = Ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 执行参数化事务语句
        /// </summary>
        /// <param name="sqlDirectionary"></param>
        /// <returns></returns>
        public static bool ExecuteTransSql(Dictionary<String, List<CustomSqlParam>> sqlDirectionary)
        {
            try
            {
                int affectcount = CommonDal.ExecuteTransSql(sqlDirectionary);
                Information = "操作成功";
                return true;
            }
            catch (ConnectionException Ex)
            {
                Information = Ex.Message;
                return false;
            }
            catch (DataAccessExcption Ex)
            {
                Information = Ex.Message;
                return false;
            }
            catch (Exception Ex)
            {
                Errors.WriteLog(Ex.Message, Ex);
                Information = Ex.Message;
                return false;
            }
        }
    }
}
