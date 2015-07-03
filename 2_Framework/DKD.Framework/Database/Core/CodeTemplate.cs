//using System.Text;
//using DKD.Framework.Database.Attribute;

//namespace DKD.Framework.Database.Core
//{
//    public class CodeTemplate<TObjectType>
//    {
//        #region 字段

//        private string _classSource = @"using System;
//                                        using System.Data;
//                                        using System.Text;
//                                        using System.Data.SqlClient;
//                                        using System.Collections.Generic;
//                                        using DKD.Framework.Database.DbHelper;
//                                        using DKD.PackageField.Search;
//                                        using DKD.Framework.Extensions;
//                                        using DKD.PackageField.Update;
//                                        using DKD.PackageField.Fileds;
//                                        using DKD.Querying;
//                                        using Model;
//                                        namespace {NameSpace}
//                                        {
//	                                        /// <summary>
//	                                        /// {ModelName}数据访问类，动态生成
//	                                        /// </summary>
//	                                        public class {ModelName}
//	                                        {
//		                                        public {ModelName}(){}
//                                                {Exists}
//                                                {Add}
//                                                {Update}
//                                                {UpdatePart}
//                                                {Delete} 
//                                                {GetList}
//                                                {GetList1}
//                                                {GetList2}
//                                                {DeleteWhere}
//                                                {GetModel}
//                                                {GetModel1} 
//                                                {GetModel2}     
//                                                {GetCount}                                             
//	                                        }
//                                        }
//                                        ";


//        private readonly TableInfo _tableInfo;
//        private readonly ColumnAttribute _primaryKey = AttributeHelper.GetPrimaryKey<TObjectType>();

//        #endregion

//        #region 对外方法

//        public CodeTemplate()
//        {
//            _tableInfo = AttributeHelper.GetInfo<TObjectType>();
//        }

//        public string GetCodeSource()
//        {
//            _classSource = _classSource.Replace("{ModelName}", _tableInfo.Table.Name)
//                          .Replace("{Exists}", CreateExists())
//                          .Replace("{Add}", CreateAdd())
//                          .Replace("{Update}", CreateUpdate())
//                          .Replace("{UpdatePart}", CreateUpdatePart())
//                          .Replace("{Delete}", CreateDelete())
//                          .Replace("{DeleteWhere}", CreateDeleteWhere())
//                          .Replace("{GetList}", CreateGetList())
//                          .Replace("{GetList1}", CreateGetList1())
//                          .Replace("{GetList2}", CreateGetListFieds())
//                          .Replace("{GetModel}", CreateGetModel())
//                          .Replace("{GetModel1}", CreateGetModel1())
//                          .Replace("{GetModel2}", CreateGetModel2())
//                          .Replace("{GetCount}", CreateCount())
//                          .Replace("{NameSpace}", Config.ConfigBase.Instance<Config.FrameworkConfig>().Namespace);

//            return _classSource;
//        }

//        #endregion

//        #region 生成类方法代码

//        private string CreateExists()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine(" public bool Exists" + string.Format("({0} {1})", AttributeHelper.PrimaryKeyTypeToCs(_primaryKey), _primaryKey.Name) + "{");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            //创建SQL语句头
//            source.AppendLine(string.Format("strSql.Append(\"select count(1) from {0}\");", _tableInfo.Table.Name));

//            //创建参数列表
//            source.AppendLine(string.Format("strSql.Append(\" where {0}=@{0} \");", _primaryKey.Name));

//            //创建参数赋值
//            source.AppendLine("SqlParameter[] parameters = {new SqlParameter(\"@" + _primaryKey.Name + "\", SqlDbType." + _primaryKey.Type.ToString() + ")};parameters[0].Value = " + _primaryKey.Name + ";");

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.Exists(strSql.ToString(), parameters);");

//            source.AppendLine("}");

//            return source.ToString();

//        }

//        private string CreateAdd()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine("public int Add(" + _tableInfo.TypeFullName + " model){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            //创建SQL语句头
//            source.AppendLine("strSql.Append(\"insert into " + _tableInfo.Table.Name + "(\");");

//            //创建列名列表
//            source.AppendLine("strSql.Append(\"" + AttributeHelper.GetColunsString(_tableInfo.Columns, true) + ")\"" + ");");

//            source.AppendLine("strSql.Append(\" values (\");");

//            //创建列名值列表
//            source.AppendLine(string.Format("strSql.Append(\"{0})\");", AttributeHelper.GetValuesString(_tableInfo.Columns, true)));

//            source.AppendLine("strSql.Append(\";select @@IDENTITY\");");

//            //创建参数列表
//            source.AppendLine("SqlParameter[] parameters = {" + AttributeHelper.GetSqlParameterDeclare(_tableInfo.Columns, true) + "};");

//            //创建参数赋值
//            source.AppendLine(AttributeHelper.GetSqlParameterValue(_tableInfo.Columns, true));

//            //创建执行命令
//            source.AppendLine("object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);");

//            source.AppendLine("return obj == null ? 1 : Convert.ToInt32(obj);");

//            source.AppendLine("}");

//            return source.ToString();
//        }

//        private string CreateUpdate()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine("public bool Update(" + _tableInfo.TypeFullName + "  model){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            //创建SQL语句头
//            source.AppendLine(string.Format("strSql.Append(\"update {0} set \");", _tableInfo.Table.Name));

//            //创建更新字段列表
//            source.AppendLine(AttributeHelper.GetUpdateColumns(_tableInfo.Columns));

//            //创建参数列表
//            source.AppendLine("SqlParameter[] parameters = {" + AttributeHelper.GetSqlParameterDeclare(_tableInfo.Columns) + "};");

//            //创建参数赋值
//            source.AppendLine(AttributeHelper.GetSqlParameterValue(_tableInfo.Columns));

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters)>0;");

//            source.AppendLine("}");


//            return source.ToString();
//        }

//        private string CreateUpdatePart()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine("public bool UpdatePart(object primaryKeyValue,UpdateDepot<" + _tableInfo.TypeFullName + "> update){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            source.AppendLine("var updateData= new UpdateCriteria<" + _tableInfo.TypeFullName + ">(update);");
            
//            //创建SQL语句头
//            source.AppendLine(string.Format("strSql.Append(\"UPDATE {0} SET \"+updateData.GetFiledValue);", _tableInfo.Table.Name));

//            source.AppendLine(string.Format("strSql.Append(\" WHERE {0}='\"+primaryKeyValue+\"'\");", _primaryKey.Name));

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.ExecuteSql(strSql.ToString(), new SqlParameter())>0;");

//            source.AppendLine("}");


//            return source.ToString();
//        }

//        private string CreateDelete()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine(" public bool Delete(" + AttributeHelper.PrimaryKeyTypeToCs(_primaryKey) + " " + _primaryKey.Name + "){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            //创建SQL语句头
//            source.AppendLine(string.Format("strSql.Append(\"delete from {0} \");", _tableInfo.Table.Name));

//            //创建以主键为条件语句
//            source.AppendLine(AttributeHelper.GetPrimaryKeyWhere(_primaryKey));

//            //创建参数列表
//            source.AppendLine("SqlParameter[] parameters = {" + AttributeHelper.GetSqlParameterDeclare(new[] { _primaryKey }) + "};");

//            //创建参数赋值
//            source.AppendLine(AttributeHelper.GetSqlParameterValue(new[] { _primaryKey }).Replace("model.", ""));

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters)>0;");

//            source.AppendLine("}");

//            return source.ToString();
//        }

//        private string CreateDeleteWhere()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine(" public bool DeleteWhere(SearchDepot<" + _tableInfo.TypeFullName + "> search){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            source.AppendFormat("var searchData = new SearchCriteria<" + _tableInfo.TypeFullName + ">(search);");

//            //创建SQL语句头
//            source.AppendLine(string.Format("strSql.Append(\"delete from {0} \");", _tableInfo.Table.Name));

//            //创建删除条件语句
//            source.AppendLine("strSql.Append(\" where \"+ searchData.ToWhereClause());");

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.ExecuteSql(strSql.ToString())>0;");

//            source.AppendLine("}");

//            return source.ToString();
//        }

//        private string CreateGetList()
//        {
//            StringBuilder source = new StringBuilder();

//            source.AppendLine("public DataSet GetList(SearchDepot<" + _tableInfo.TypeFullName + "> search){");

//            source.AppendLine("return GetList(int.MaxValue,search);");

//            source.AppendLine("}");


//            return source.ToString();
//        }

//        private string CreateGetList1()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine("public DataSet GetList(int Top, SearchDepot<" + _tableInfo.TypeFullName +
//                              "> search,FiledsDepot<" + _tableInfo.TypeFullName + "> fileds = null){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            source.AppendFormat("var searchData = new SearchCriteria<" + _tableInfo.TypeFullName + ">(search);");

//            source.AppendFormat("var orderData = new OrderCriteria<" + _tableInfo.TypeFullName + ">(search);");

//            source.AppendLine("string filedsStr=\"*\";");

//            source.AppendLine("if(fileds!=null){");

//            source.AppendLine("FiledsCriteria<" + _tableInfo.TypeFullName + "> filedsModel = new FiledsCriteria<" + _tableInfo.TypeFullName + ">(fileds);");

//            source.AppendLine("filedsStr=filedsModel.GetFiledValue;}");

//            source.AppendFormat("var filedOrder = orderData.GetOrderStr;");

//            source.AppendFormat("var strWhere =searchData.ToWhereClause();");

//            //创建SQL语句头
//            source.AppendLine("strSql.Append(\"select \");");

//            //创建Top
//            source.AppendLine("if (Top > 0) strSql.Append(\" top \" + Top.ToString());");

//            //创建字段列表
//            source.AppendLine("strSql.Append(filedsStr);");

//            //创建From
//            source.AppendLine(string.Format("strSql.Append(\" FROM {0} \");", _tableInfo.Table.Name));

//            //创建Where
//            source.AppendLine("if (strWhere.Trim() != \"\") strSql.Append(\" where \" + strWhere);");

//            //创建Order
//            source.AppendLine("if(filedOrder.Trim()!=\"\")strSql.Append(\" order by \" + filedOrder);");

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.Query(strSql.ToString());");

//            source.AppendLine("}");


//            return source.ToString();
//        }

//        private string CreateCount()
//        {
//            var source = new StringBuilder();

//            //创建方法头
//            source.AppendLine("public int GetCount(SearchDepot<" + _tableInfo.TypeFullName + "> search){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            source.AppendFormat("var searchData = new SearchCriteria<" + _tableInfo.TypeFullName + ">(search);");

//            source.AppendFormat("var strWhere =searchData.ToWhereClause();");

//            //创建SQL语句头
//            source.AppendLine("strSql.Append(\"select Count(*) \");");

//            //创建From
//            source.AppendLine(string.Format("strSql.Append(\" FROM {0} \");", _tableInfo.Table.Name));

//            //创建Where
//            source.AppendLine("if (strWhere.Trim() != \"\") strSql.Append(\" where \" + strWhere);");

//            //创建执行命令
//            source.AppendLine("return (int)DbHelperSQL.GetSingle(strSql.ToString());");

//            source.AppendLine("}");

//            return source.ToString();
//        }


//        private string CreateGetModel()
//        {
//            StringBuilder sources = new StringBuilder();

//            sources.AppendLine("public " + _tableInfo.TypeFullName + " GetModel(" + AttributeHelper.PrimaryKeyTypeToCs(_primaryKey) + " " + _primaryKey.Name + ",FiledsDepot<" + _tableInfo.TypeFullName + "> fileds=null){");

//            sources.AppendLine("DataSet dst = new DataSet();");

//            sources.AppendLine("StringBuilder strSql = new StringBuilder();");

//            sources.AppendLine("strSql.Append(\"SELECT \");");

//            sources.AppendLine("string filedsStr=\"*\";");

//            sources.AppendLine("if(fileds!=null){");

//            sources.AppendLine("FiledsCriteria<"+_tableInfo.TypeFullName+"> filedsModel = new FiledsCriteria<"+_tableInfo.TypeFullName+">(fileds);");

//            sources.AppendLine("filedsStr=filedsModel.GetFiledValue;}");

//            sources.AppendLine("strSql.Append(filedsStr);");

//            sources.AppendLine("strSql.Append(\" FROM " + _tableInfo.Table.Name + " WHERE \");");

//            sources.AppendLine("strSql.Append(\"" + _primaryKey.Name + "=\"+ID.ToString());");

//            sources.AppendLine("dst=DbHelperSQL.Query(strSql.ToString());");

//            sources.AppendLine("if (dst.Tables[0].Rows.Count < 1) return null;");

//            sources.AppendLine("return dst.Tables[0].ToList<" + _tableInfo.TypeFullName + ">()[0];");

//            sources.AppendLine("}");

//            return sources.ToString();
//        }


//        private string CreateGetModel1()
//        {
//            StringBuilder sources = new StringBuilder();

//            sources.AppendLine("public List<" + _tableInfo.TypeFullName + " > GetModels(SearchDepot<" +
//                               _tableInfo.TypeFullName + "> search,FiledsDepot<" + _tableInfo.TypeFullName +
//                               "> fileds=null){");
//            sources.AppendLine("DataSet dst = new DataSet();");
//            sources.AppendLine("dst=GetList(int.MaxValue,search,fileds);");
//            sources.AppendLine("if (dst.Tables[0].Rows.Count < 1) return null;");
//            sources.AppendLine("return dst.Tables[0].ToList<" + _tableInfo.TypeFullName + ">();");

//            sources.AppendLine("}");

//            return sources.ToString();
//        }


//        private string CreateGetModel2()
//        {
//            StringBuilder sources = new StringBuilder();

//            sources.AppendLine("public List<" + _tableInfo.TypeFullName + " > GetModelsByTop(int Top, SearchDepot<" +
//                               _tableInfo.TypeFullName + "> search,FiledsDepot<" + _tableInfo.TypeFullName +
//                               "> fileds=null){");
//            sources.AppendLine("DataSet dst = new DataSet();");
//            sources.AppendLine("dst=GetList(Top,search,fileds);");
//            sources.AppendLine("if (dst.Tables[0].Rows.Count < 1) return null;");
//            sources.AppendLine("return dst.Tables[0].ToList<" + _tableInfo.TypeFullName + ">();");

//            sources.AppendLine("}");

//            return sources.ToString();
//        }

//        private string CreateGetListFieds()
//        {
//            StringBuilder source = new StringBuilder();

//            //创建方法头
//            source.AppendLine("public DataSet GetListFieds(int Top, SearchDepot<" + _tableInfo.TypeFullName +
//                              "> search,FiledsDepot<" + _tableInfo.TypeFullName + "> fileds=null){");

//            source.AppendLine("StringBuilder strSql = new StringBuilder();");

//            source.AppendFormat("var searchData = new SearchCriteria<" + _tableInfo.TypeFullName + ">(search);");

//            source.AppendFormat("var orderData = new OrderCriteria<" + _tableInfo.TypeFullName + ">(search);");

//            source.AppendLine("string filedsStr=\"*\";");

//            source.AppendLine("if(fileds!=null){");

//            source.AppendLine("FiledsCriteria<" + _tableInfo.TypeFullName + "> filedsModel = new FiledsCriteria<" + _tableInfo.TypeFullName + ">(fileds);");

//            source.AppendLine("filedsStr=filedsModel.GetFiledValue;}");

//            //创建SQL语句头
//            source.AppendLine("strSql.Append(\"select \");");

//            //创建Top
//            source.AppendLine("if (Top > 0) strSql.Append(\" top \" + Top.ToString()+\" \");");

//            //创建字段列表
//            source.AppendLine("strSql.Append(filedsStr);");

//            //创建From
//            source.AppendLine(string.Format("strSql.Append(\" FROM {0} \");", _tableInfo.Table.Name));

//            source.AppendLine("var strWhere=searchData.ToWhereClause();");

//            source.AppendLine("var filedOrder=orderData.GetOrderStr;");

//            //创建Where
//            source.AppendLine("if (strWhere.Trim() != \"\") strSql.Append(\" where \" + strWhere);");

//            //创建Order
//            source.AppendLine("if(filedOrder.Trim()!=\"\")strSql.Append(\" order by \" + filedOrder);");

//            //创建执行命令
//            source.AppendLine("return DbHelperSQL.Query(strSql.ToString());");

//            source.AppendLine("}");


//            return source.ToString();

//        }

//        #endregion
//    }
//}