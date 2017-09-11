using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Kalendar.Zero.Utility.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {


        /// <summary>
        /// 随机排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oriList"></param>
        /// <returns></returns>
        public static List<T> RandomResortList<T>(this List<T> oriList)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            if (oriList != null)
            {
                foreach (T item in oriList)
                {
                    newList.Insert(random.Next(newList.Count), item);
                }
            }
            return newList;
        }

        /// <summary>
        /// SQL过滤
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SQLParse(this string text)
        {
            var sqlExp = new Regex(@"\s*\'\s+|\s(and|exec|insert|select|delete|update|count|drop|table|\*|\%|chr|mid|master|truncate|char|declare)\s");

            return sqlExp.Replace(text+"", "").Trim();
        }

        #region Json - T

        /// <summary>
        /// Toes the json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return ToJson(obj, null);
        }
        /// <summary>
        /// Toes the json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="jsonConverters">The json converters.</param>
        /// <returns></returns>
        public static string ToJson(this object obj, IEnumerable<JavaScriptConverter> jsonConverters)
        {
            var serializer = new JavaScriptSerializer {MaxJsonLength = Int32.MaxValue};
            if (jsonConverters != null) serializer.RegisterConverters(jsonConverters);
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// DataTable专用转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DtToJson(this DataTable obj)
        {
            var serializer = new JavaScriptSerializer {MaxJsonLength = Int32.MaxValue};
            var dic = new ArrayList();
            foreach (DataRow row in obj.Rows)
            {
                DataRow dr = row;
                var drow = obj.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                dic.Add(drow);
            }
            return serializer.Serialize(dic);
        }


        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer {MaxJsonLength = Int32.MaxValue};
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static T JsonToObj<T>(this string str)
        {
            var serializer = new JavaScriptSerializer {MaxJsonLength = Int32.MaxValue};
            return serializer.Deserialize<T>(str);
        }

        /// <summary>
        /// 命名约束
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToJsonContract(this object obj)
        {
            var result = "";
            var serializer =
                new DataContractJsonSerializer(obj.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                result = sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T JsonToObjContract<T>(this string str)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            var serializer =
                new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        #endregion

        #region XML - object

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] Serialize(this object value)
        {
            var ms = new MemoryStream();
            var bf1 = new BinaryFormatter();
            bf1.Serialize(ms, value);
            return ms.ToArray();
        }

        /// <summary>
        /// Serializes the XML file.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void SerializeXmlFile(this object o, string fileName)
        {
            var serializer = new XmlSerializer(o.GetType());
            if (!File.Exists(fileName)) return;
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write)) serializer.Serialize(stream, o);
        }
        /// <summary>
        /// Deserializes the XML file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static T DeserializeXmlFile<T>(string fileName)
        {
            T o;
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read)) o = (T)serializer.Deserialize(stream);
            return o;
        }

        /// <summary>
        /// Serializes the XML.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static string SerializeXml(this object o)
        {
            var serializer = new XmlSerializer(o.GetType());
            var stringBuilder = new StringBuilder();
            using (TextWriter textWriter = new StringWriter(stringBuilder)) serializer.Serialize(textWriter, o);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Deserializes the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T DeserializeXml<T>(this string xml)
        {
            return (T)Deserialize(xml, typeof(T));
        }
        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object Deserialize(string xml, Type type)
        {
            object o;
            var serializer = new XmlSerializer(type);
            using (TextReader textReader = new StringReader(xml)) o = serializer.Deserialize(textReader);
            return o;
        }

        #endregion

        #region DataTable - List

        /// <summary>
        /// 转化一个DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            //创建属性的集合
            var pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            var dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                T o = item;
                pList.ForEach(p => row[p.Name] = p.GetValue(o, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            var oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                var ob = new T();
                //找到对应的数据  并赋值
                DataRow dr = row;
                prlist.ForEach(p => { if (dr[p.Name] != DBNull.Value) p.SetValue(ob, dr[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }


        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTableTow(IList list)
        {
            var result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                foreach (object t in list)
                {
                    var tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /**/
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable(list, null);
        }

        /**/
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            var propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);

            var result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                foreach (T t in list)
                {
                    var tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(t, null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(t, null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        #endregion

        #region 实体类型转换

        /// <summary>
        /// Converts to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object value) { return value.ConvertTo(default(T)); }
        /// <summary>
        /// Converts to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType)) return (T)converter.ConvertTo(value, targetType);
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    try { if (converter.CanConvertFrom(value.GetType())) return (T)converter.ConvertFrom(value); }
                    catch { }
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// Converts to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="ignoreException">if set to <c>true</c> [ignore exception].</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (ignoreException)
            {
                try
                {
                    return value.ConvertTo<T>();
                }
                catch
                {
                    return defaultValue;
                }
            }
            return value.ConvertTo<T>();
        }
        
        #endregion

        #region 变量类型转换

        /// <summary>
        /// Toes the int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static int ToInt(this object strValue, int defValue) { int def; int.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        /// <summary>
        /// Toes the tiny int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static byte ToTinyInt(this object strValue, byte defValue) { byte def; byte.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        /// <summary>
        /// Toes the small int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static short ToSmallInt(this object strValue, short defValue) { short def; short.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        /// <summary>
        /// Toes the decimal.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object strValue, decimal defValue) { decimal def; decimal.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        /// <summary>
        /// Toes the float.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static float ToFloat(this object strValue, float defValue) { float def; float.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        /// <summary>
        /// Toes the big int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static Int64 ToBigInt(this object strValue, Int64 defValue) { Int64 def; Int64.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        
		
		/// <summary>
        /// Toes the money.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static decimal ToMoney(this object strValue, decimal defValue) { decimal def; decimal.TryParse(strValue.ToString(), out def); return def == 0 ? defValue : def; }
        /// <summary>
        /// Toes the integer.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static int ToInteger(this object strValue, int defValue)
        {
            int def;
            int.TryParse(strValue.ToString(), out def);
            return def == 0 ? defValue : def;
        }
        /// <summary>
        /// Toes the bool.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="defValue">if set to <c>true</c> [def value].</param>
        /// <returns></returns>
        public static bool ToBool(this object expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression.ToString(), "true", true) == 0) return true;
                if (string.Compare(expression.ToString(), "false", true) == 0) return false;
                if (string.Compare(expression.ToString(), "1", true) == 0) return true;
                if (string.Compare(expression.ToString(), "0", true) == 0) return false;
            }
            return defValue;
        }

        /// <summary>
        /// Toes the date time.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object strValue,DateTime defValue)
        {
            DateTime def;
            DateTime.TryParse(strValue.ToString(), out def);
            return def == Convert.ToDateTime( null) ? defValue : def;
        }

        /// <summary>
        /// Toes the tiny int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static byte ToTinyInt(this object strValue) { return strValue.ToTinyInt(0); }
        /// <summary>
        /// Toes the small int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static short ToSmallInt(this object strValue) { return strValue.ToSmallInt(0); }
        /// <summary>
        /// Toes the decimal.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object strValue) { return strValue.ToDecimal(0); }
        /// <summary>
        /// Toes the float.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static float ToFloat(this object strValue) { return strValue.ToFloat(0); }
        /// <summary>
        /// Toes the big int.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static Int64 ToBigInt(this object strValue) { return strValue.ToBigInt(0); }
        /// <summary>
        /// Toes the money.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static decimal ToMoney(this object strValue) { return strValue.ToMoney(0); }
        /// <summary>
        /// Toes the integer.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static int ToInteger(this object strValue) { return strValue.ToInteger(0); }

        /// <summary>
        /// Toes the date time.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object strValue) { return strValue.ToDateTime(DateTime.Now); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int ToInt(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static long ToInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Md5(this object obj)
        {
            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(obj+""))).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 转换失败 将赋值为-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int StringToInt(this string obj)
        {
            int i;
            if (int.TryParse(obj, out i))
            {
                return i;
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDl(this object value)
        {
            if (value != null)
            {
                return ((IConvertible)value).ToDecimal(null);
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this object value)
        {
            try
            {
                if (value != null && value.ToString() != "")
                {
                    return ((IConvertible) value).ToInt32(null);
                }
            }catch(Exception ex) { }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this object value)
        {
            if (value != null)
            {
                return ((IConvertible)value).ToBoolean(null);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToTime(this object value)
        {
            if (value != null && value.ToString() != "")
            {
                return ((IConvertible)value).ToDateTime(null);
            }
            return new DateTime(1800, 1, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultTime"></param>
        /// <returns></returns>
        public static DateTime ToTime(this object value, DateTime defaultTime)
        {
            if (value != null && value.ToString() != "")
            {
                return ((IConvertible)value).ToDateTime(null);
            }
            return defaultTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamel(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToLower() + s.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToPascal(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToUpper() + s.Substring(1);
        }
        #endregion

        #region Invoke

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod<object>(obj, methodName, parameters);
        }
        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static T InvokeMethod<T>(this object obj, string methodName)
        {
            return InvokeMethod<T>(obj, methodName, null);
        }
        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName);

            if (method == null) throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);

            var value = method.Invoke(obj, parameters);
            return (value is T ? (T)value : default(T));
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName, null);
        }
        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return GetPropertyValue(obj, propertyName, default(T));
        }
        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null) throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);

            var value = property.GetValue(obj, null);
            return (value is T ? (T)value : defaultValue);
        }
        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null) throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);

            property.SetValue(obj, value, null);
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            return GetAttribute<T>(obj, true);
        }
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="includeInherited">if set to <c>true</c> [include inherited].</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            if ((attributes.Length > 0))
            {
                return (attributes[0] as T);
            }
            return null;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj) where T : Attribute
        {
            return GetAttributes<T>(obj);
        }
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="includeInherited">if set to <c>true</c> [include inherited].</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            return type.GetCustomAttributes(typeof(T), includeInherited).OfType<T>().Select(attribute => attribute);
        }

        /// <summary>
        /// Determines whether the specified obj is type.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified obj is type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsType(this object obj, Type type)
        {
            return obj.GetType().Equals(type);
        }
        /// <summary>
        /// Toes the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ToType<T>(this object value) { return (T)value; }
        /// <summary>
        /// Determines whether the specified obj is array.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        ///   <c>true</c> if the specified obj is array; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsArray(this object obj)
        {
            return obj.IsType(typeof(Array));
        }
        /// <summary>
        /// Determines whether [is DB null] [the specified obj].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        ///   <c>true</c> if [is DB null] [the specified obj]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDBNull(this object obj)
        {
            return obj.IsType(typeof(DBNull));
        }

        #endregion

        #region Null Check

        /// <summary>
        /// Checks the on null.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void CheckOnNull(this object @this, string parameterName)
        {
            if (@this.IsNull()) throw new ArgumentNullException(parameterName);
        }
        /// <summary>
        /// Checks the on null.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public static void CheckOnNull(this object @this, string parameterName, string message)
        {
            if (@this.IsNull()) throw new ArgumentNullException(parameterName, message);
        }
        /// <summary>
        /// Determines whether the specified @this is null.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns>
        ///   <c>true</c> if the specified @this is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this object @this)
        {
            return @this == null;
        }
        /// <summary>
        /// Determines whether [is not null] [the specified @this].
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns>
        ///   <c>true</c> if [is not null] [the specified @this]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNull(this object @this)
        {
            return !@this.IsNull();
        }

        #endregion

        /// <summary>
        /// Unsafes the cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T UnsafeCast<T>(this object value)
        {
            return value.IsNull() ? default(T) : (T)value;
        }
        /// <summary>
        /// Safes the cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T SafeCast<T>(this object value)
        {
            return value is T ? value.UnsafeCast<T>() : default(T);
        }
        /// <summary>
        /// Instances the of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool InstanceOf<T>(this object value)
        {
            return value is T;
        }
        
        /// <summary>
        /// Writes the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        public static void Write(this object o) {
            //Msg.Write(o); 
        }
        /// <summary>
        /// Writes the end.
        /// </summary>
        /// <param name="o">The o.</param>
        public static void WriteEnd(this object o) {
            //Msg.WriteEnd(o); 
        }

		 #region byte
        
        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Decode(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        /// <summary>
        /// Hashes the specified data.使用指定算法Hash
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hashName">Name of the hash.</param>
        /// <returns></returns>
        public static byte[] Hash(this byte[] data, string hashName)
        {
            HashAlgorithm algorithm = string.IsNullOrEmpty(hashName) ? HashAlgorithm.Create() : HashAlgorithm.Create(hashName);
            return algorithm.ComputeHash(data);
        }

        /// <summary>
        /// Hashes the specified data.使用默认算法Hash
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static byte[] Hash(this byte[] data)
        {
            return Hash(data, null);
        }

        /// <summary>
        /// Gets the bit.获取取第index是否为1,index从0开始
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }
        /// <summary>
        /// Sets the bit.将第index位设为1
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }

        /// <summary>
        /// 将第index位设为0
        /// Clears the bit.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }

        /// <summary>
        /// Reverses the bit.
        /// 将第index位取反
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }

        /// <summary>
        /// Saves the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="path">The path.</param>
        public static void Save(this byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }


        /// <summary>
        /// Toes the memory stream.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }

        #endregion
        
        #region 字符串处理  例如正则

        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>
        /// 全角字符串
        /// </returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>
        /// 半角字符串
        /// </returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }


        /// <summary>
        /// Determines whether the specified s is match.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <c>true</c> if the specified s is match; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// Matches the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }

        #endregion

        #region 随机数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static bool NextBool(this Random random)
        {
            return random.NextDouble() > 0.5;
        }

        /*
        //enum Shape { Ellipse, Rectangle, Triangle }
        //Shape shape = random.NextEnum<Shape>();
        */

        /// <summary>
        /// 随机枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            var data = new byte[length];
            random.NextBytes(data);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static UInt16 NextUInt16(this Random random)
        {
            return BitConverter.ToUInt16(random.NextBytes(2), 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Int16 NextInt16(this Random random)
        {
            return BitConverter.ToInt16(random.NextBytes(2), 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static float NextFloat(this Random random)
        {
            return BitConverter.ToSingle(random.NextBytes(4), 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 时间戳转换时间
        /// 10位秒级* 10000000
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        public static DateTime TimespanToDateTime(this string inVal)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(inVal + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 13位毫秒级*10000
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(this long inVal)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = new TimeSpan(inVal*10000);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 时间转换时间戳（秒）
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        public static long DateTimeToTimespan(this DateTime inVal)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(inVal - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间转换时间戳（毫秒）
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        public static long DateTimeToTimestamp(this DateTime inVal)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(inVal - startTime).TotalMilliseconds;
        }

        #endregion

        #region Dictionary<TKey, TValue>

        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }

        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="values">The values.</param>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }
        #endregion
        
        #region WhereIf

        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        #endregion
        
        #region IsBetween
        /// <summary>
        /// Determines whether the specified t is between.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="includeLowerBound">if set to <c>true</c> [include lower bound].</param>
        /// <param name="includeUpperBound">if set to <c>true</c> [include upper bound].</param>
        /// <returns>
        ///   <c>true</c> if the specified t is between; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound,
        bool includeLowerBound = false, bool includeUpperBound = false)
        where T : class, IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }

        /// <summary>
        /// Determines whether the specified t is between.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="includeLowerBound">if set to <c>true</c> [include lower bound].</param>
        /// <param name="includeUpperBound">if set to <c>true</c> [include upper bound].</param>
        /// <returns>
        ///   <c>true</c> if the specified t is between; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, IComparer<T> comparer,
        bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");

            var lowerCompareResult = comparer.Compare(t, lowerBound);
            var upperCompareResult = comparer.Compare(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        #endregion
        
        #region Distinct 扩展方法
        /// <summary>
        /// Distincts the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TV>(this IEnumerable<T> source, Func<T, TV> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, TV>(keySelector));
        }

        /// <summary>
        /// Distincts the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV">The type of the V.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TV>(this IEnumerable<T> source, Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, TV>(keySelector, comparer));
        }
        #endregion

        #region Expression 扩展

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression AndAlso(this Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Expression Call(this Expression instance, string methodName, params Expression[] arguments)
        {
            return Expression.Call(instance, instance.Type.GetMethod(methodName), arguments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression GreaterThan(this Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="body"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body, params  ParameterExpression[] parameters)
        {
            return Expression.Lambda<TDelegate>(body, parameters);
        }
        #endregion

        #region FillFrom

        /// <summary>
        /// 实体数据填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T FillFrom<T>(this T obj, object source) where T : class, new()
        {
            if (source != null)
            {
                PropertyInfo[] pis = typeof (T).GetProperties();
                PropertyInfo[] pisSource = source.GetType().GetProperties();

                foreach (PropertyInfo pi in pis)
                {
                    try
                    {
                        foreach (var propertyInfo in pisSource)
                        {
                            if (propertyInfo.Name == pi.Name)
                            {
                                object value = propertyInfo.GetValue(source, null);
                                pi.SetValue(obj, value, null);

                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return obj;
            }
            return new T();
        }

        /// <summary>
        /// 从FormCollection填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public static T FillFromCollection<T>(this T obj, FormCollection formCollection) where T : class, new()
        {
            PropertyInfo[] pis = typeof(T).GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    if (formCollection[pi.Name] != null)
                    {
                        object value = formCollection[pi.Name] ?? "";

                        if (pi.PropertyType == typeof(int))
                            value = value.ToInt();
                        if (pi.PropertyType == typeof(decimal))
                            value = value.ToDecimal();
                        if (pi.PropertyType == typeof(int?))
                            value = value.ToInt();
                        if (pi.PropertyType == typeof(bool))
                            value = ((value + "") != "false");
                        if (pi.PropertyType == typeof(DateTime))
                            value = value.ToDateTime();

                        pi.SetValue(obj, value, null);
                    }
                }
                catch (Exception) { }
            }
            return obj;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV">The type of the V.</typeparam>
	public class CommonEqualityComparer<T, TV> : IEqualityComparer<T>
    {
        private Func<T, TV> keySelector;
        private IEqualityComparer<TV> comparer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        public CommonEqualityComparer(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        public CommonEqualityComparer(Func<T, TV> keySelector)
            : this(keySelector, EqualityComparer<TV>.Default)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
        

    }
}
