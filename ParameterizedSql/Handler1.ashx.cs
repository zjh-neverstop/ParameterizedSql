using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BLL;
using Common;

namespace ParameterizedSql
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            /*
             * 一般情况下，我们都会通过orm的方式来获取数据，但是有些场景下需要通过执行手写的sql语句来获取结果，这时候，如果直接拼接sql语句，可能会
             * 出现sql注入，为了避免这种情况，我们可以使用参数化语句，例如：
             * SqlCommand command = new SqlCommand(sql);
               SqlParameterCollection sqlParams = command.Parameters;
               sqlParams.Add("@FieldList", SqlDbType.VarChar);
               ......此处省略

               sqlParams[0].Value = FieldList;
               ......此处省略
             * 然而这回造成另外一个问题，因为上面那种写法通常应该写在数据层，但是拼接sql字符串应该在控制层或者业务层，为了解决这个问题，可以通过
             * 自定义一个封装相关参数的类，在拼接sql字符串的时候，将参数封装到一个列表中，然后传递到数据层，在数据层中对参数列表进行解析，上面的
             * 语句就变成如下写法：
             * SqlCommand command = new SqlCommand(sql);
               command.CommandType = CommandType.Text;

               SqlParameterCollection sqlParams = command.Parameters;
               foreach (var sqlParam in paramList)
               {
                   sqlParams.Add(sqlParam.Name, sqlParam.Type);
                   sqlParams[sqlParam.Name].Value = sqlParam.Value;
               }
             * 通过这种写法可以很好的实现控制层与数据层的隔离
             * 这个示例只是做个演示，有些dll没有引用，无法运行
             */


            //定义参数化sql语句
            string sqlstr = "select * from yourTable where yourFiled1=@fieldValue1 and yourField2=@fieldValue2";

            List<CustomSqlParam> sqlParamList = new List<CustomSqlParam>();

            //封装参数
            sqlParamList.Add(new CustomSqlParam
            {
                Name = "@fieldValue1",
                Type = SqlDbType.NVarChar,
                Value = "theFieldValue"
            });
            sqlParamList.Add(new CustomSqlParam
            {
                Name = "@fieldValue2",
                Type = SqlDbType.Int,
                Value = 0
            });

            DataSet ds = CommonBll.GetDataByParameterizedSql(sqlParamList, sqlParamList);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
