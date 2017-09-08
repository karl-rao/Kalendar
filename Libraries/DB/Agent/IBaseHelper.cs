using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Kalendar.Zero.DB.Agent
{
    /// <summary>
    /// 一些基本的，作为辅助函数的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseHelper<T> where T : Entity.Base.BaseEntity
	{
	
        #region 删除
        
        /// <summary>
        /// 根据条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByCondition(
            string condition, 
            DbConnection connection, 
            DbTransaction transaction);

        #endregion

        #region 添加
        
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns>
        /// 执行成功返回True
        /// </returns>
        T Insert(
            T obj, 
            DbConnection connection, 
            DbTransaction transaction);

        #endregion

        #region 修改

        /// <summary>
        /// Updates the specified obj.
        /// </summary>
        /// <param name="ori">The obj.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Update(
            T ori,
            T obj, 
            DbConnection connection, 
            DbTransaction transaction);

        #endregion

        #region 查询

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        T FindById(
            object keyValue,
            DbConnection connection,
            DbTransaction transaction);

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        T FindSingle(
            string condition,
            DbConnection connection, 
            DbTransaction transaction);
        
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        List<T> Find(
            string condition, 
            DbConnection connection, 
            DbTransaction transaction);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderField"></param>
        /// <param name="isDescending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        List<T> Find(
            string orderField, 
            bool isDescending, 
            int pageSize,
            int pageNo, 
            DbConnection connection, 
            DbTransaction transaction);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <param name="orderField"></param>
        /// <param name="isDescending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        List<T> Find(
            string condition, 
            IDbDataParameter[] paramList, 
            string orderField, 
            bool isDescending, 
            int pageSize, 
            int pageNo, 
            DbConnection connection, 
            DbTransaction transaction);
        
        /// <summary>
        /// Gets the struct set.under connection or connection+trasaction
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <param name="orderField"></param>
        /// <param name="isDescending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Entity.Ext.StructSet<T> GetStructSet(
            string condition,
            IDbDataParameter[] paramList,
            string orderField,
            bool isDescending,
            int pageSize,
            int pageNo,
            DbConnection connection, 
            DbTransaction transaction);

        #endregion
        
        #region DB操作
        
        /// <summary>
        /// Get the list.under connection or connection+trasaction
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="paramList"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        List<T> GetList(
            string sql, 
            CommandType commandType,
            IDbDataParameter[] paramList,
            DbConnection connection, 
            DbTransaction transaction);
        
        /// <summary>
        /// Gets the DataSet.under connection or connection+trasaction
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="paramList"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        DataSet GetDataSet(
            string sql, 
            CommandType commandType, 
            IDbDataParameter[] paramList, 
            DbConnection connection, 
            DbTransaction transaction);

        /// <summary>
        /// Executes the scalar. under connection or connection+trasaction
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="paramList"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        object ExecuteScalar(
            string sql, 
            CommandType commandType, 
            IDbDataParameter[] paramList, 
            DbConnection connection, 
            DbTransaction transaction);

        /// <summary>
        /// Executes the specified SQL.under connection or connection+trasaction
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="paramList"></param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Execute(
            string sql, 
            CommandType commandType, 
            IDbDataParameter[] paramList, 
            DbConnection connection,
            DbTransaction transaction);
        
        #endregion
	}
}