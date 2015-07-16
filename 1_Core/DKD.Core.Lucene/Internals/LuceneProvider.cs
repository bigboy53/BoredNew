using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using DKD.Framework.Extensions;
using DKD.Framework.Logger;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace DKD.Core.Lucene
{
    public class LuceneProvider : BaseLucene, ILuceneProvider
    {

        public void Add(LuceneModel model)
        {
            model.IndexType = LuceneType.Insert;
            LuceneModels.Enqueue(model);
        }

        public void Delete(int id, int type)
        {
            var model = new LuceneModel
            {
                ID = id,
                Type = type,
                IndexType = LuceneType.Delete
            };
            LuceneModels.Enqueue(model);
        }

        public void Edit(LuceneModel model)
        {
            model.IndexType = LuceneType.Modify;
            LuceneModels.Enqueue(model);
        }

        public void StartNewThread()
        {
            ThreadPool.QueueUserWorkItem(QueueToLucene);
        }

        public List<LuceneModel> GetList(string keyword,int? type, int pageIndex, int pageSize, out int dataCount)
        {
            dataCount = 0;
            var result = new List<LuceneModel>();
            try
            {
                var ky = keyword;
                IndexSearcher searcher;
                searcher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(LucenePath)), true);
                var bq = new BooleanQuery();
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = SplitContent.SplitWords(keyword, new PanGuTokenizer());
                    var queryParser = new QueryParser("content", new PanGuAnalyzer(true));
                    var query = queryParser.Parse(keyword);
                    var titleQueryParser = new QueryParser("title", new PanGuAnalyzer(true));
                    var titleQuery = titleQueryParser.Parse(keyword);
                    bq.Add(query, BooleanClause.Occur.SHOULD);
                    //表示条件关系为“or”,BooleanClause.Occur.MUST表示“and”,BooleanClause.Occur.MUST_NOT表示“not”
                    bq.Add(titleQuery, BooleanClause.Occur.SHOULD);
                }
                else
                {
                    Query query = new WildcardQuery(new Term("title", "*"));
                    bq.Add(query, BooleanClause.Occur.SHOULD);
                }
                if (type.HasValue)
                {
                    Query query = new WildcardQuery(new Term("type", type.Value.ToString()));
                    bq.Add(query, BooleanClause.Occur.SHOULD);
                }

                var sort = new Sort(new SortField("createtime", SortField.INT, true));
                var docs = searcher.Search(bq, null, pageSize * 1000, sort);


                //var collector = TopScoreDocCollector.create(1000, true);
                //searcher.Search(bq, null, collector);
                //dataCount = collector.GetTotalHits();//返回总条数
                //var docs = collector.TopDocs((pageIndex - 1) * pageSize, pageSize).scoreDocs;//取前十条数据  可以通过它实现搜索结果分页

                ////创建一个结果收集器（收集结果最大数为1000页）
                //TopScoreDocCollector collector = TopScoreDocCollector.create(pageSize * 1000, true);
                //searcher.Search(bq, null, collector);
                //TopDocs topDoc = collector.TopDocs(0, collector.GetTotalHits());
                ////搜索结果总数超出指定收集器大小，则摈弃
                //if (topDoc.totalHits > pageSize * 1000)
                //    recCount = pageSize * 1000;
                //else
                //    recCount = topDoc.totalHits;

                //搜索结果总数超出指定收集器大小，则摈弃
                if (docs.totalHits > pageSize * 1000)
                    dataCount = pageSize * 1000;
                else
                    dataCount = docs.totalHits;
                int i = (pageIndex - 1) * pageSize;
                while (i < dataCount && result.Count < pageSize)
                {
                    var model = new LuceneModel();
                    Document doc = searcher.Doc(docs.scoreDocs[i].doc);

                    model.Content = doc.Get("content");
                    model.HightLightContent = SplitContent.HightLight(ky, doc.Get("content"));
                    model.Title = doc.Get("title");
                    model.HightLightTitle = SplitContent.HightLight(ky, doc.Get("title"));
                    model.ID = Convert.ToInt32(doc.Get("id"));
                    model.ClickCount = Convert.ToInt32(doc.Get("clickcount"));
                    model.Images = doc.Get("images");
                    model.Tags = doc.Get("tags");
                    model.Type = Convert.ToInt32(doc.Get("type"));
                    model.CreateTime = DateTimeExtension.UnixToDateTime(Convert.ToInt32(doc.Get("createtime")));
                    result.Add(model);

                    #region 循环中有try catch 非常消耗性能 解决方案 报错不处理
                    //try
                    //{
                    //    model.Content = doc.Get("content");
                    //    model.HightLightContent = SplitContent.HightLight(ky, doc.Get("content"));
                    //    model.Title = doc.Get("title");
                    //    model.HightLightTitle = SplitContent.HightLight(ky, doc.Get("title"));
                    //    model.ID = Convert.ToInt32(doc.Get("id"));
                    //    model.ClickCount = Convert.ToInt32(doc.Get("clickcount"));
                    //    model.Images = doc.Get("images");
                    //    model.Tags = doc.Get("tags");
                    //    model.Type = Convert.ToInt32(doc.Get("type"));
                    //    model.CreateTime = DateTimeExtension.UnixToDateTime(Convert.ToInt32(doc.Get("createtime")));
                    //    result.Add(model);
                    //}
                    //catch (Exception e)
                    //{
                    //    LoggerHelper.Logger(
                    //        string.Format("Lucene搜索错误：ID:{0},Type:{1},Title:{2},CreateTime:{3}", doc.Get("id"),
                    //            doc.Get("type"), doc.Get("title"), doc.Get("createtime")), e);
                    //}
                    //finally
                    //{
                    //    i++;
                    //}
                    #endregion

                    i++;

                }
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.Logger("Lucene中GetList()错误", ex);
                throw new LuceneException.LuceneException("Lucene中GetList()错误",ex);
            }
        }

    }
}
