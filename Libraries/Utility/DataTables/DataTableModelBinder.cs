﻿using System;
using System.Reflection;
using System.Web.Mvc;
using Kalendar.Zero.Utility.DataTables;

namespace Kalendar.Zero.Utility.DataTables
{
    /// <summary>
    /// This type is the ModelBinder for jquery.datatables
    /// Add this type to MVC modelbinders to bind jquery.datatables
    /// requests to <see cref="DataTable"/>
    /// </summary>
    public class DataTableModelBinder : IModelBinder {

        /// <summary>
        /// 
        /// </summary>
        public static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region IModelBinder Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {

            Logger.Info("Bind Request");

            if (controllerContext == null) {
                throw new ArgumentNullException("controllerContext");
            }


            if (bindingContext == null) {
                throw new ArgumentNullException("bindingContext");
            }

            DataTable dataTable = new DataTable();

            //see http://datatables.net/usage/server-side
            string sEcho = bindingContext.ValueProvider.GetValue("sEcho").AttemptedValue;
            if (string.IsNullOrEmpty(sEcho)) {
                throw new ArgumentException("sEcho must always be provided");
            }
            dataTable.sEcho = sEcho;

            string iDisplayStartString = bindingContext.ValueProvider.GetValue("iDisplayStart").AttemptedValue;
            dataTable.iDisplayStart = int.Parse(iDisplayStartString);

            string iDisplayLengthString = bindingContext.ValueProvider.GetValue("iDisplayLength").AttemptedValue;
            dataTable.iDisplayLength = int.Parse(iDisplayLengthString);

            string iColumnsString = bindingContext.ValueProvider.GetValue("iColumns").AttemptedValue;
            dataTable.iColumns = int.Parse(iColumnsString);

            ValueProviderResult sSearchResult = bindingContext.ValueProvider.GetValue("sSearch");
            if (sSearchResult != null) {
                dataTable.sSearch = sSearchResult.AttemptedValue;
            }

            ValueProviderResult bEscapeRegexResult = bindingContext.ValueProvider.GetValue("bEscapeRegex");
            if (bEscapeRegexResult != null) {
                string bEscapeRegexString = bEscapeRegexResult.AttemptedValue;
                bool bEscapeRegex;
                if (!string.IsNullOrEmpty(bEscapeRegexString) &&
                     bool.TryParse(bEscapeRegexString, out bEscapeRegex)) {
                    dataTable.bEscapeRegex = bEscapeRegex;
                }
            }

            for (int i = 0; i < dataTable.iColumns; i++) {
                bool bSortables = false;
                ValueProviderResult bSortableResult = bindingContext.ValueProvider.GetValue(string.Format("bSortable_{0}", i));
                if (bSortableResult != null) {
                    string bSortablesString = bSortableResult.AttemptedValue;
                    bool.TryParse(bSortablesString, out bSortables);
                }
                dataTable.bSortables.Add(bSortables);
            }

            for (int i = 0; i < dataTable.iColumns; i++) {
                bool bSearchables = false;
                ValueProviderResult bSearchableResult = bindingContext.ValueProvider.GetValue(string.Format("bSearchable_{0}", i));
                if (bSearchableResult != null) {
                    string bSearchablesString = bSearchableResult.AttemptedValue;
                    bool.TryParse(bSearchablesString, out bSearchables);
                }
                dataTable.bSearchables.Add(bSearchables);
            }

            for (int i = 0; i < dataTable.iColumns; i++) {
                string sSearchsString = string.Empty;
                ValueProviderResult sSearch_Result = bindingContext.ValueProvider.GetValue(string.Format("sSearch_{0}", i));
                if (sSearch_Result != null) {
                    sSearchsString = sSearch_Result.AttemptedValue;
                }
                dataTable.sSearchs.Add(sSearchsString);
            }

            for (int i = 0; i < dataTable.iColumns; i++) {
                bool bEscapeRegexs = false;
                ValueProviderResult bEscapeRegexsResult = bindingContext.ValueProvider.GetValue(string.Format("bEscapeRegex_{0}", i));
                if (bEscapeRegexsResult != null) {
                    string bEscapeRegexsString = bEscapeRegexsResult.AttemptedValue;
                    bool.TryParse(bEscapeRegexsString, out bEscapeRegexs);
                }
                dataTable.bEscapeRegexs.Add(bEscapeRegexs);
            }

            string iSortingColsString = bindingContext.ValueProvider.GetValue("iSortingCols").AttemptedValue;
            dataTable.iSortingCols = int.Parse(iSortingColsString);

            for (int i = 0; i < dataTable.iSortingCols; i++) {
                string iSortColsString = bindingContext.ValueProvider.GetValue(string.Format("iSortCol_{0}", i)).AttemptedValue;
                dataTable.iSortCols.Add(int.Parse(iSortColsString));
            }

            for (int i = 0; i < dataTable.iSortingCols; i++) {
                string sSortDirString = bindingContext.ValueProvider.GetValue(string.Format("sSortDir_{0}", i)).AttemptedValue;
                if (sSortDirString == "asc") {
                    dataTable.sSortDirs.Add(DataTableSortDirection.Ascending);
                } else {
                    dataTable.sSortDirs.Add(DataTableSortDirection.Descending);
                }
            }

            return dataTable;
        }

        #endregion
    }
    
}