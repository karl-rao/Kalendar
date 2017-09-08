using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Kalendar.Zero.DB.Agent
{
    /// <summary>
    /// 事务定义
    /// </summary>
    public class Transaction : IDisposable
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// 链接字符串
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; } = Config.ConnectionMssqlString;

        private DbConnection conn;
        private DbTransaction _dbTrans;

        /// <summary>
        /// Gets the db connection.
        /// </summary>
        public DbConnection DbConnection => conn;

        /// <summary>
        /// Gets the db trans.
        /// </summary>
        public DbTransaction DbTrans
        {
            get { return _dbTrans; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        public Transaction()
        {
            conn = new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="cfgConnectionString">The CFG connection string.</param>
        public Transaction(string cfgConnectionString)
        {
            ConnectionString = cfgConnectionString;
            conn = new SqlConnection(cfgConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Transaction(SqlConnection connection)
        {
            conn = connection;
        }

        /// <summary>
        /// Begins this instance.
        /// </summary>
        public void Begin()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            _dbTrans = conn.BeginTransaction();
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            _dbTrans.Commit();
        }

        /// <summary>
        /// Rolls the back.
        /// </summary>
        public void RollBack()
        {
            _dbTrans.Rollback();
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Colses this instance.
        /// </summary>
        public void Close()
        {
            if ((conn != null) && (conn.State == System.Data.ConnectionState.Open))
            {
                conn.Close();
            }
            _dbTrans?.Dispose();
        }
    }
}