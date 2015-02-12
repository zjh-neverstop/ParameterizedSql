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

        public static DataSet GetDataByParameterizedSql(string sql, List<CustomSqlParam> paramList)
        {
            try
            {
                return CommonDal.GetDataByParameterizedSql(sql, paramList);
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
    }
}
