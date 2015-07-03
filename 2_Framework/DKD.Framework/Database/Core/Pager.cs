//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;
//using DKD.Framework.Database.Attribute;
//using DKD.Framework.Database.DbHelper;
//using DKD.Framework.Extensions;
//using DKD.Mappings;
//using DKD.PackageField.Search;
//using DKD.Querying;
//using Microsoft.VisualBasic.ApplicationServices;

//namespace DKD.Framework.Database.Core
//{
//    /// <summary>
//    /// 通用数据分页类
//    /// </summary>
//    public class Pager<T> where T : class, new()
//    {
//        private const string PageProcedure = "DKDPager";

//        #region 属性与私有变量

//        /// <summary>
//        /// 数据排序方式
//        /// </summary>
//        public enum OrderTypes
//        {
//            降序 = 1,
//            升序 = 0
//        }

//        private string _tableName = "";
//        private string _primaryKey = "";

//        private int _pageIndex = 0;
//        private int _pageSize = 20;
//        private IList<T> _records;
//        private int _totalRecords = 0;
//        private string _fileds="";
//        private string _orderStr;

//        /// <summary>
//        /// 当前数据分页
//        /// </summary>
//        public int PageIndex
//        {
//            get
//            {
//                return this._pageIndex;
//            }
//        }

//        /// <summary>
//        /// 分页数据大小
//        /// </summary>
//        public int PageSize
//        {
//            get
//            {
//                return this._pageSize;
//            }
//        }

//        /// <summary>
//        /// 分页数据
//        /// </summary>
//        public IList<T> Records
//        {
//            get
//            {
//                if (this._records == null)
//                {
//                    this._records = new List<T>();
//                }
//                return this._records;
//            }
//        }

//        public string Fileds
//        {
//            get
//            {
//                return _fileds;
//            }
//        }

//        /// <summary>
//        /// 总共有多少条数据
//        /// </summary>
//        public int TotalRecords
//        {
//            get
//            {
//                return this._totalRecords;
//            }
//        }


//        /// <summary>
//        /// 排序(ID DESC)
//        /// </summary>
//        public string OrderStr
//        {
//            get { return _orderStr; }
//        }


//        /// <summary>
//        /// 是否能显示分页
//        /// </summary>
//        public bool CanShowPager { get { return this.TotalRecords > this.PageSize; } }

//        #endregion

//        #region 构造函数与方法

//        /// <summary>
//        /// 创建分页数据
//        /// </summary> 
//        /// <param name="search">条件模型</param>
//        /// <param name="pageSize">分页大小</param>
//        /// <param name="pageIndex">当前分页号码</param>
//        /// <param name="fileds"></param>
//        public Pager(SearchDepot<T> search, int pageSize, int pageIndex, string fileds="*")
//        {
//            InitDate(search, pageSize, pageIndex, fileds);
//        }

//        /// <summary>
//        /// 初始化分页数据
//        /// </summary>
//        /// <param name="search"></param>
//        /// <param name="pageSize">分页大小</param>
//        /// <param name="pageIndex">当前分页号码</param>
//        /// <param name="fileds">字段</param>
//        private void InitDate(SearchDepot<T> search, int pageSize, int pageIndex, string fileds)
//        {
//            _tableName = Config.FrameworkConfig.Instance<Config.FrameworkConfig>().TablePrefix + typeof(T).Name;

//            _primaryKey = AttributeHelper.GetPrimaryKeyString<T>();

//            this._pageSize = pageSize < 1 ? 1 : pageSize;

//            this._pageIndex = pageIndex < 1 ? 1 : pageIndex;

//            var searchModel= new SearchCriteria<T>(search);

//            var order = new OrderCriteria<T>(search);

//            _orderStr = order.GetOrderStr;
//            if (string.IsNullOrEmpty(_orderStr))
//            {
//                _orderStr = "ORDER BY "+_primaryKey + " DESC";
//            }

//            this._records = new List<T>();

//            this._fileds = fileds;

//            this._records = GetList(_pageSize, _pageIndex, searchModel.ToWhereClause(), _primaryKey, _orderStr, out _totalRecords, _fileds).Tables[0].ToList<T>();
//        }

//        /// <summary>
//        /// 分页获取数据列表
//        /// </summary>
//        public DataSet GetList(int PageSize, int PageIndex, string strWhere, string primaryKey, string orderStr, out int dataCount, string fileds)
//        {
//            var totalCount = new SqlParameter { ParameterName = "@TotalCount", Direction = ParameterDirection.Output, DbType = DbType.Int32, SqlValue = 0 };
//            SqlParameter[] parameters = {
//                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
//                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
//                    new SqlParameter("@PageSize", SqlDbType.Int),
//                    new SqlParameter("@PageIndex", SqlDbType.Int),
//                    totalCount,
//                    new SqlParameter("@strWhere", SqlDbType.VarChar,4000),
//                    new SqlParameter("@ordStr", SqlDbType.VarChar, 255),
//                    new SqlParameter("@fldNames", SqlDbType.VarChar, 1000),                    
//                    };
//            parameters[0].Value = _tableName;
//            parameters[1].Value = primaryKey;
//            parameters[2].Value = PageSize;
//            parameters[3].Value = PageIndex;
//            parameters[4].Value = 0;
//            parameters[5].Value = strWhere;
//            parameters[6].Value = orderStr;
//            parameters[7].Value = fileds;

//            var data = DbHelperSQL.RunProcedure(PageProcedure, parameters, "ds");
//            dataCount = (int)totalCount.Value;
//            return data;
//        }

//        #endregion

//        #region 视图分页
//        /// <summary>
//        /// 链表分页
//        /// </summary>
//        /// <param name="joinTable">表联合查询例：IP_Address as am LEFT JOIN Area as apON am.code=ap.code</param>
//        /// <param name="fields">查询列必须显示指定表和列 不能为空 例：am.code,am.city </param>
//        /// <param name="search">条件模型</param>
//        /// <param name="pageIndex">当前页数</param>
//        /// <param name="pageSize">每页记录数</param>
//        /// <param name="isGetTotalCount">是否查询总条数(非分页时使用)</param>
//        /// <param name="groupStr">分组字段 可为空</param>
//        /// <param name="useGroup">是否使用分组</param>
//        public Pager(string joinTable, string fields, SearchDepot<T> search, int pageIndex, int pageSize, bool isGetTotalCount=true,string groupStr = "", bool useGroup = false)
//        {
//            InitDate(joinTable, fields, search, pageIndex, pageSize, isGetTotalCount, groupStr, useGroup);
//        }

//        private void InitDate(string joinTable, string fields, SearchDepot<T> search, int pageIndex, int pageSize, bool isGetTotalCount,string groupStr, bool useGroup)
//        {
//            _tableName = Config.FrameworkConfig.Instance<Config.FrameworkConfig>().TablePrefix + typeof(T).Name;

//            _primaryKey = AttributeHelper.GetPrimaryKeyString<T>();

//            this._pageSize = pageSize < 1 ? 1 : pageSize;

//            this._pageIndex = pageIndex < 1 ? 1 : pageIndex;

//            var searchModel = new SearchCriteria<T>(search);

//            var order = new OrderCriteria<T>(search);

//            _orderStr = order.GetOrderStr;
//            if (string.IsNullOrEmpty(_orderStr))
//            {
//                _orderStr = _primaryKey + "DESC";
//            }

//            this._records = new List<T>();

//            this._fileds = fields;
//            var seavice = new BaseService.BaseService();
//            this._records = seavice.GetPageByJoin<T>(joinTable, Fileds, _orderStr, pageIndex, pageSize,
//                searchModel.ToWhereClause(),
//                out _totalRecords, isGetTotalCount, groupStr, useGroup);
//        }
//        #endregion

//        #region 数据分页处理

//        /// <summary>
//        /// 显示分页控件,默认显示10个
//        /// </summary>
//        /// <param name="helper">HtmlHelper对象</param>
//        public void RenderPager(HtmlHelper helper)
//        {
//            RenderPager(helper, 10);
//        }

//        /// <summary>
//        /// 显示分页控件
//        /// </summary>
//        /// <param name="helper">HtmlHelper对象</param>
//        /// <param name="showPage">显示多少个分页数</param>
//        public void RenderPager(HtmlHelper helper, int? showPage)
//        {
//            ChildActionExtensions.RenderAction(helper, "PagerControl", "Pager", new { recordCount = TotalRecords, pageSize = PageSize, pageIndex = PageIndex, showPage = showPage });
//        }

//        #endregion
//    }

//    /// <summary>
//    /// 分页控件Controller
//    /// </summary>
//    public class PagerController : BaseController
//    {

//        /// <summary>
//        /// 分页控件
//        /// </summary>
//        /// <param name="pageSize">分页大小</param>
//        /// <param name="recordCount">记录总数</param>
//        /// <param name="pageIndex">当前分页号</param>
//        /// <param name="showPage">分页控件显示多少个分页数</param>
//        /// <returns></returns>
//        public ActionResult PagerControl(int? pageSize, int recordCount, int? pageIndex, int? showPage)
//        {
//            Dictionary<string, int> pageDate = new Dictionary<string, int>();


//            pageDate.Add("PageSize", pageSize == null ? 20 : pageSize.Value);

//            pageDate.Add("RecordCount", recordCount);

//            pageDate.Add("PageIndex", pageIndex == null ? StringExtensions.IsInt(Request.QueryString["Page"]) ? int.Parse(Request.QueryString["Page"]) : 1 : pageIndex.Value);

//            pageDate.Add("ShowPage", showPage == null ? 10 : showPage.Value);

//            return View(ControlPath + "PagerControl.aspx", pageDate);
//        }
//    }
//}
