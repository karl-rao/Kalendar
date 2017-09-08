using System;
using System.Data.SqlClient;

namespace Kalendar.Utility.Example
{
	/// <summary>
    /// 链接范例
    /// </summary>
    public class Connection
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
        /// Hows to use.
        /// </summary>
        public void HowToUse()
        {
            DB.DAL.Base.Connection myConnection = null;

            try
            {
                myConnection = new DB.DAL.Base.Connection();
                myConnection.Open();
                var connection = (SqlConnection)myConnection.DbConnection;

                /*Database Operation using one connection*/
            }
            catch (Exception ex)
            {
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
