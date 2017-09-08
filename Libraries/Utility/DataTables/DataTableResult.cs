﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kalendar.Zero.Utility.DataTables;

namespace Kalendar.Zero.Utility.DataTables
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTableResult:ActionResult
    {/// <summary>
        /// An unaltered copy of sEcho sent from the client side. 
        /// This parameter will change with each draw (it is basically a draw count) - 
        /// so it is important that this is implemented. Note that it strongly recommended 
        /// for security reasons that you 'cast' this parameter to an integer 
        /// in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>        
        [DataMember(Name = "sEcho")]
        public string sEcho { get; private set; }

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        [DataMember(Name = "iTotalRecords")]
        public int iTotalRecords { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned in this result set)
        /// </summary>
        [DataMember(Name = "iTotalDisplayRecords")]
        public int iTotalDisplayRecords { get; set; }

        /// <summary>
        /// Optional - this is a string of column names, comma separated (used in combination with sName) 
        /// which will allow DataTables to reorder data on the client-side if required for display
        /// </summary>
        [DataMember(Name = "sColumns")]
        public string sColumns { get; set; }

        /// <summary>
        /// The data in a 2D array
        /// Fill this structure with the plain table data
        /// represented as string.
        /// </summary>
        [DataMember(Name = "aaData")]
        public List<List<string>> aaData { get; set; }

        [IgnoreDataMember]
        public Encoding ContentEncoding { get; set; }

        [IgnoreDataMember]
        public string ContentType { get; set; }

        [IgnoreDataMember]
        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sEcho"></param>
        /// <param name="iTotalRecords"></param>
        /// <param name="iTotalDisplayRecords"></param>
        /// <param name="aaData"></param>
        public DataTableResult(string sEcho, int iTotalRecords, int iTotalDisplayRecords, List<List<string>> aaData)
        {
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            this.sEcho = sEcho;
            this.iTotalRecords = iTotalRecords;
            this.iTotalDisplayRecords = iTotalDisplayRecords;
            this.aaData = aaData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="iTotalRecords"></param>
        /// <param name="iTotalDisplayRecords"></param>
        /// <param name="aaData"></param>
        public DataTableResult(DataTable dataTable, int iTotalRecords, int iTotalDisplayRecords, List<List<string>> aaData)
            : this(dataTable.sEcho, iTotalRecords, iTotalDisplayRecords, aaData)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        public DataTableResult(DataTable dataTable)
            : this(dataTable.sEcho, 0, 0, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private DataTableResult()
            : this(string.Empty, 0, 0, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Get not allowed");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType)
                ? this.ContentType 
                : "application/json";
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            var serializer = new DataContractJsonSerializer(typeof(DataTableResult));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            string json = response.ContentEncoding.GetString(ms.ToArray());

            response.Write(json);
        }
    }
}