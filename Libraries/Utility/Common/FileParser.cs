using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kalendar.Zero.Utility.Common
{
    public class FileParser
    {

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="charset">The charset.</param>
        /// <returns></returns>
        public static string ReadFile(string path, string charset)
        {
            string result;
            string templateFile = path;
            if (File.Exists(templateFile))
            {
                Encoding absoluteEncoding = Encoding.GetEncoding(charset);
                var stream = new StreamReader(templateFile, absoluteEncoding);
                result = stream.ReadToEnd();
                stream.Close();
            }
            else
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 写锁
        /// </summary>
        private static ReaderWriterLockSlim WriteLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="content">The content.</param>
        /// <param name="charset">The charset.</param>
        /// <returns></returns>
        public static string WriteFile(string path, string content, string charset)
        {
            string result;
            try
            {
                WriteLock.EnterReadLock();

                Encoding encoding = Encoding.GetEncoding(charset);
                var stream = new StreamWriter(path, false, encoding);
                stream.Write(content);
                stream.Close();

                result = path;
            }
            catch (Exception)
            {
                result = "";
            }
            finally
            {
                WriteLock.ExitReadLock();
            }

            return result;
        }

    }
}
