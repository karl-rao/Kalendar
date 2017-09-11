using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Kalendar.Zero.DB.Entity.Base;

namespace Kalendar.Zero.DB.Agent
{
	/// <summary>
    /// 数据访问层的基类
    /// </summary>
    public class MssqlHelper<T> : IBaseHelper<T> where T : Entity.Base.BaseEntity, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region 实例属性

	    /// <summary>
        /// 链接字符串
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
	    public string ConnectionString { get; set; } = Config.ConnectionMssqlString;


	    /// <summary>
        /// 排序字段
        /// </summary>
        /// <value>
        /// The sort field.
        /// </value>
        public string SortField { get; set; } = "UpdateTime";

	    /// <summary>
        /// 选择的字段，默认为所有(*)
        /// </summary>
        /// <value>
        /// The selected fields.
        /// </value>
        public string SelectedFields { get; set; } = " * ";

	    /// <summary>
        /// 是否为降序
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is descending; otherwise, <c>false</c>.
        /// </value>
        public bool IsDescending { get; set; } = true;

	    /// <summary>
        /// 数据库访问对象的表名
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
		public string TableName { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 实例化
        /// 获取实体属性数据（表名、主键、排序字段）
        /// </summary>
        public MssqlHelper()
	    {
            var attributes= typeof (T).GetCustomAttributes(typeof(TableSettingsAttribute),true);
	        foreach (TableSettingsAttribute attribute in attributes)
	        {
	            TableName = attribute.TableName;
	            SortField = attribute.SortField;
	            PrimaryKey = attribute.PrimaryKey;
	        }
        }
        
		#endregion
        
        #region 子类必须实现的函数(用于更新或者插入)

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// (提供了默认的反射机制获取信息，为了提高性能，建议重写该函数)
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>
        /// 实体类对象
        /// </returns>
        protected virtual T DataReaderToEntity(IDataReader dr)
        {
            var obj = new T();
            var builder = DynamicBuilder<T>.CreateBuilder(dr);
            while (dr.Read())
            {
                obj = builder.Build(dr);
            }

            return obj;
        }

        /// <summary>
        /// 将从数据库中获取的数据行转化为指定的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr">The dr.</param>
        /// <returns></returns>
        protected virtual T FillEntityFromDataReader(SqlDataReader dr)
        {
            var builder = DynamicBuilder<T>.CreateBuilder(dr);
            var instance = builder.Build(dr);

            return instance;
        }

        /// <summary>
        /// Fills the entity from reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr">The dr.</param>
        /// <returns></returns>
        protected virtual List<T> FillEntitiesFromReader(SqlDataReader dr)
        {
            var list = new List<T>();
            DynamicBuilder<T> builder = DynamicBuilder<T>.CreateBuilder(dr);

            using (SqlDataReader reader = dr)
            {
                while (reader.Read())
                {
                    var instance = builder.Build(reader);
                    if (!list.Contains(instance))
                        list.Add(instance);
                }
            }

            return list;
        }

        /// <summary>
        /// 将从数据库中获取的数据行转化为指定的实体
        /// </summary>
        /// <typeparam name="T">转化的实体</typeparam>
        /// <param name="dr">指定转化的数据行</param>
        /// <returns>转化后的实体列表</returns>
        public static T FillEntityFromDataRow(DataRow dr)
        {
            var instance = Activator.CreateInstance<T>();
            foreach (PropertyInfo pinfo in typeof(T).GetProperties())
            {
                if (dr.Table.Columns.IndexOf(pinfo.Name) >= 0 && dr[pinfo.Name] != null && dr[pinfo.Name] != DBNull.Value)
                {
                    try
                    {
                        pinfo.SetValue(instance,
                                       pinfo.PropertyType.BaseType != typeof(Enum)
                                           ? Convert.ChangeType(dr[pinfo.Name], pinfo.PropertyType)
                                           : Enum.Parse(pinfo.PropertyType, dr[pinfo.Name].ToString()), null);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Fills the entity from data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tb">The tb.</param>
        /// <returns></returns>
        public static List<T> FillEntitiesFromDataTable(DataTable tb)
        {
            var list = new List<T>();

            foreach (DataRow dr in tb.Rows)
            {
                var o = FillEntityFromDataRow(dr);
                if (!list.Contains(o))
                    list.Add(o);
            }

            return list;
        }

        #endregion

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="condition"></param>
	    /// <param name="connection"></param>
	    /// <param name="transaction"></param>
	    /// <returns></returns>
	    public bool DeleteByCondition(
	        string condition,
	        DbConnection connection,
	        DbTransaction transaction)
	    {
	        try
	        {
	            string sql = $"DELETE FROM [{TableName}] WHERE {condition} ";
	            return Execute(sql, CommandType.Text, null, connection, transaction);
	        }
	        catch (Exception ex)
	        {
	            Logger.Error(ex);
	        }
	        return false;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
	    public T Insert(
	        T obj,
	        DbConnection connection,
	        DbTransaction transaction)
        {
            var param = new List<IDbDataParameter>();

            var fields = "";
	        var values = "";

	        var getScopeIdentity = false;

            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo t in pis)
            {
                foreach (FieldSettingsAttribute attribute in t.GetCustomAttributes(typeof(FieldSettingsAttribute), true))
                {
                    var objValue = t.GetValue(obj, null) ?? DBNull.Value;
                    if (attribute.InsertRequired)
                    {
                            if (fields != "")
                            {
                                fields += " , ";
                                values += " , ";
                            }
                            fields += $" [{t.Name}] ";
                            values += $" @{t.Name} ";

                            param.Add(new SqlParameter($"@{t.Name}", objValue));
                    }

                    if (attribute.IsIdentity)
                    {
                        getScopeIdentity = true;
                    }
                }
            }

            string sql = $"INSERT INTO {TableName} ({fields}) VALUES ({values})";
            sql += getScopeIdentity ? " Select CAST(ISNULL(SCOPE_IDENTITY() ,0) As int)" : "Select 0";
            var objKeyValue = ExecuteScalar(sql, CommandType.Text, param.ToArray(), connection, transaction);
	        if (getScopeIdentity)
	        {
	            foreach (PropertyInfo t in pis)
                {
                    foreach (
                        FieldSettingsAttribute attribute in t.GetCustomAttributes(typeof (FieldSettingsAttribute), true)
                        )
                    {
                        if (attribute.IsPrimaryKey && attribute.IsIdentity)
                        {
                            t.SetValue(obj, objKeyValue, null);
                        }
                    }
                }
	        }

	        return obj;
        }

        /// <summary>
        /// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
        /// </summary>
        /// <param name="ori">原对象</param>
        /// <param name="obj">更新对象</param>
        /// <param name="connection">The conn.</param>
        /// <param name="transaction">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        /// <returns></returns>
        public bool Update(T ori,T obj, DbConnection connection, DbTransaction transaction)
        {
            var param = new List<IDbDataParameter>();

            var fields = "";
            var condition = "";

            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo t in pis)
            {
                foreach (FieldSettingsAttribute attribute in t.GetCustomAttributes(typeof(FieldSettingsAttribute), true))
                {
                    var oriValue=t.GetValue(ori, null) ?? DBNull.Value;
                    var objValue = t.GetValue(obj, null) ?? DBNull.Value;
                    if (!oriValue.Equals(objValue) && attribute.UpdateRequired)
                    {
                        if (fields != "")
                            fields += " , ";
                        fields += string.Format(" [{0}]=@{0} ", t.Name);

                        param.Add(new SqlParameter($"@{t.Name}", objValue));
                    }

                    if (attribute.IsPrimaryKey)
                    {
                        if (condition != "")
                            condition += " AND ";

                        condition += string.Format(" [{0}]=@{0} ", t.Name);
                        param.Add(new SqlParameter($"@{t.Name}", objValue));
                    }
                }
                
            }
            
            string sql = $"UPDATE {TableName} SET {fields} WHERE {condition} ";
            return Execute(sql, CommandType.Text, param.ToArray(), connection, transaction);
        }
        
        /// <summary>
        /// Finds the by ID.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public T FindById(
            object keyValue,
            DbConnection connection,
            DbTransaction transaction)
        {
            string filter = $"[{PrimaryKey}]='{keyValue}'";
            return FindSingle(filter, connection, transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public T FindSingle(
            string condition,
            DbConnection connection,
            DbTransaction transaction)
        {
            T entity = null;
            List<T> list = Find(condition, null, SortField, true, 1, 1, connection, transaction);
            if (list.Count > 0)
            {
                entity = list[0];
            }
            return entity;
        }
        
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="orderField">The order field.</param>
        /// <param name="isDescending">if set to <c>true</c> [is descending].</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public List<T> Find(string orderField, bool isDescending, int pageSize, int pageNo, DbConnection connection, DbTransaction transaction)
        {
            var sql = GetSql(TableName, " 1=1 ", SelectedFields, orderField, isDescending, pageSize, pageNo);
            return GetList(sql, CommandType.Text, null, connection, transaction);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<T> Find(string condition, DbConnection connection, DbTransaction transaction)
        {
            return Find(condition, null, true, connection, transaction);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="paramList">The param list.</param>
        /// <param name="needSort">The needSort list.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns>
        /// 指定对象的集合
        /// </returns>
        public List<T> Find(
            string condition,
            IDbDataParameter[] paramList,
            bool needSort,
            DbConnection connection,
            DbTransaction transaction)
        {
            string sql = $"Select {SelectedFields} From [{TableName}]  WITH (NOLOCK) Where ";
            sql += condition;
            if (needSort)
                sql += $" Order by {SortField} {(IsDescending ? "DESC" : "")}";

            return GetList(sql, CommandType.Text, paramList, connection, transaction);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="paramList">The param list.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="isDescending">if set to <c>true</c> [is descending].</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public List<T> Find(
            string condition,
            IDbDataParameter[] paramList,
            string orderField,
            bool isDescending,
            int pageSize,
            int pageNo,
            DbConnection connection,
            DbTransaction transaction)
        {
            var sql = GetSql(TableName, condition, SelectedFields, orderField, isDescending, pageSize, pageNo);
            return GetList(sql, CommandType.Text, paramList, connection, transaction);
        }

        /// <summary>
        /// Gets the struct set.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="paramList">The param list.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="isDescending">if set to <c>true</c> [is descending].</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public Entity.Ext.StructSet<T> GetStructSet(
            string condition,
            IDbDataParameter[] paramList,
            string orderField,
            bool isDescending,
            int pageSize,
            int pageNo,
            DbConnection connection,
            DbTransaction transaction)
        {
            var sql = GetSql(TableName,condition,SelectedFields,orderField,isDescending,pageSize,pageNo);
            sql +=
                $" Select Count(1) As RecordCount From {TableName}  WITH (NOLOCK) where {(condition == "" ? " 1=1 " : condition)}";
            var ds = GetDataSet(sql, CommandType.Text, paramList, connection, transaction);

            if (ds != null)
            {
                var ss = new Entity.Ext.StructSet<T>();
                var list = FillEntitiesFromDataTable(ds.Tables[0]);
                var count = Convert.ToInt32(ds.Tables[1].Rows[0]["RecordCount"]);

                ss.ObjectList = list;
                ss.ObjectCount = count;
                return ss;
            }
            return null;
        }

        /// <summary>
        /// 通用获取集合对象方法
        /// </summary>
        /// <param name="sql">查询的Sql语句</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="paramList">参数列表，如果没有则为null</param>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public List<T> GetList(string sql, CommandType commandType, IDbDataParameter[] paramList, DbConnection connection, DbTransaction transaction)
        {
            DateTime start = DateTime.Now;
            var list = new List<T>();

            try
            {
                var dbc =
                    transaction == null
                        ? new SqlCommand(sql, (SqlConnection)connection) { CommandType = commandType }
                        : new SqlCommand(sql, (SqlConnection)connection, (SqlTransaction)transaction) { CommandType = commandType };

                if (paramList != null)
                {
                    dbc.Parameters.AddRange(paramList);
                }

                using (IDataReader dr = dbc.ExecuteReader())
                {
                    DynamicBuilder<T> builder = DynamicBuilder<T>.CreateBuilder(dr);
                    while (dr.Read())
                    {
                        T entity = builder.Build(dr);
                        if (!list.Contains(entity))
                            list.Add(entity);
                    }
                }

                dbc.Parameters.Clear();
            }
            catch (Exception ex)
            {
                Logger.Error(sql);
                Logger.Error(ex);
            }

            Logger.Debug($"Execute {commandType} : {sql} cost {DateTime.Now.Subtract(start).TotalMilliseconds} ms.");
            return list;
        }

        /// <summary>
        /// 指定参数获取DataSet
        /// 效率比dataReader低
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="paramList">The param list.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public DataSet GetDataSet(
            string sql,
            CommandType commandType,
            IDbDataParameter[] paramList,
            DbConnection connection,
            DbTransaction transaction)
        {
            DateTime start = DateTime.Now;
            var ds = new DataSet();

            try
            {
                var dbc =
                    transaction == null
                        ? new SqlCommand(sql, (SqlConnection)connection) { CommandType = commandType }
                        : new SqlCommand(sql, (SqlConnection)connection, (SqlTransaction)transaction) { CommandType = commandType };

                if (paramList != null)
                {
                    dbc.Parameters.AddRange(paramList);
                }

                var sa = new SqlDataAdapter(dbc);
                sa.Fill(ds);
                dbc.Parameters.Clear();
            }
            catch (Exception ex)
            {
                Logger.Error(sql);
                Logger.Error(ex);
            }
            Logger.Debug($"Execute {commandType} : {sql} cost {DateTime.Now.Subtract(start).TotalMilliseconds} ms.");
            return ds;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="paramList">The param list.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, CommandType commandType, IDbDataParameter[] paramList, DbConnection connection, DbTransaction transaction)
        {
            DateTime start = DateTime.Now;
            object result;
            try
            {
                var dbc =
                    transaction == null
                        ? new SqlCommand(sql, (SqlConnection)connection) { CommandType = commandType }
                        : new SqlCommand(sql, (SqlConnection)connection, (SqlTransaction)transaction)
                        { CommandType = commandType };

                if (paramList != null)
                {
                    dbc.Parameters.AddRange(paramList);
                }

                result = dbc.ExecuteScalar();
                dbc.Parameters.Clear();
            }
            catch (Exception ex)
            {
                result = null;
                Logger.Error(sql);
                Logger.Error(ex);
            }
            Logger.Debug($"Execute {commandType} : {sql} cost {DateTime.Now.Subtract(start).TotalMilliseconds} ms.");
            return result;
        }
        
        /// <summary>
        /// 执行Sql语句，使用事务
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="paramList">The param list.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public bool Execute(string sql, CommandType commandType, IDbDataParameter[] paramList, DbConnection connection, DbTransaction transaction)
        {
            DateTime start = DateTime.Now;
            bool result = true;
            try
            {
                var dbc =
                    transaction == null
                        ? new SqlCommand(sql, (SqlConnection)connection) { CommandType = commandType }
                        : new SqlCommand(sql, (SqlConnection)connection, (SqlTransaction)transaction)
                        { CommandType = commandType };

                if (paramList != null)
                {
                    dbc.Parameters.AddRange(paramList);
                }

                dbc.ExecuteNonQuery();
                dbc.Parameters.Clear();
            }
            catch (Exception ex)
            {
                result = false;
                Logger.Error(sql);
                Logger.Error(ex);
            }
            Logger.Debug($"Execute {commandType} : {sql} cost {DateTime.Now.Subtract(start).TotalMilliseconds} ms.");
            return result;
        }

        #region SQL
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="selectedFields"></param>
        /// <param name="orderField"></param>
        /// <param name="isDescending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        private string GetSql(
            string tableName,
            string condition,
            string selectedFields,
            string orderField,
            bool isDescending,
            int pageSize,
            int pageNo)
        {
            string sql;
            if (pageSize > 0)
            {
                if (pageNo == 1)
                {
                    sql = string.Format(
                        "Select top {5} {2} From [{0}]  WITH (NOLOCK) Where {1} Order By {3} {4}",
                        tableName,
                        condition == "" ? " 1=1 " : condition,
                        selectedFields,
                        orderField,
                        isDescending ? "DESC" : "",
                        pageSize);
                }
                else
                {
                    sql = string.Format(
                        "Select * From  (Select {2},Row_Number() Over ( Order by {3} {4}) AS RowRank From [{0}]  WITH (NOLOCK) Where {1})  T ",
                        tableName,
                        condition == "" ? " 1=1 " : condition,
                        selectedFields,
                        orderField,
                        isDescending ? "DESC" : "");
                    sql += $" Where RowRank Between {(pageNo - 1)*pageSize + 1} AND {pageNo*pageSize}";
                }
            }
            else
            {
                sql = string.Format(
                    "select {2} from [{0}]  WITH (NOLOCK) Where {1} Order by {3} {4}",
                    tableName,
                    condition == "" ? " 1=1 " : condition,
                    selectedFields,
                    orderField,
                    isDescending ? "DESC" : "");
            }

            return sql;
        }


        #endregion

    }
}