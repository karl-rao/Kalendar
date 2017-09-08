using System;
using System.Data.SqlClient;

namespace Kalendar.Utility.Example
{
	/// <summary>
    /// 事务范例
    /// </summary>
    public class Transaction
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		/// <summary>
        /// Hows to use.
        /// </summary>
        public void HowToUse()
        {
            DB.DAL.Base.Connection myConnection = null;
                SqlTransaction transaction = null;

                try
                {
                    myConnection = new DB.DAL.Base.Connection();
                    myConnection.Open();
                    var connection = (SqlConnection)myConnection.DbConnection;

                    transaction = connection.BeginTransaction();

                    /*Database Operation using one connection*/

                transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    Logger.Error(ex);
                }
                finally
                {
                    if (myConnection != null)
                        myConnection.Dispose();
                }
        }
    }
}
