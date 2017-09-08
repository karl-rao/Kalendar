using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kalendar.Zero.Utility.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class StaticFilterAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Filter = new StaticFileWriteResponseFilterWrapper(filterContext.HttpContext.Response.Filter, filterContext);
        }

        class StaticFileWriteResponseFilterWrapper : System.IO.Stream
        {
            private System.IO.Stream inner;
            private ControllerContext context;
            public StaticFileWriteResponseFilterWrapper(System.IO.Stream s, ControllerContext context)
            {
                this.inner = s;
                this.context = context;
            }

            public override bool CanRead
            {
                get { return inner.CanRead; }
            }

            public override bool CanSeek
            {
                get { return inner.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return inner.CanWrite; }
            }

            public override void Flush()
            {
                inner.Flush();
            }

            public override long Length
            {
                get { return inner.Length; }
            }

            public override long Position
            {
                get
                {
                    return inner.Position;
                }
                set
                {
                    inner.Position = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return inner.Read(buffer, offset, count);
            }

            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                return inner.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                inner.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                inner.Write(buffer, offset, count);
                try
                {
                    string p = context.HttpContext.Server.MapPath(HttpContext.Current.Request.Path);

                    if (Path.HasExtension(p))
                    {
                        string dir = Path.GetDirectoryName(p);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        if (File.Exists(p))
                        {
                            File.Delete(p);
                        }
                        File.AppendAllText(p, System.Text.Encoding.UTF8.GetString(buffer));
                    }
                }
                catch
                {

                }
            }
        }
    }
}
