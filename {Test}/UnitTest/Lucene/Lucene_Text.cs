using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Lucene
{
    [TestClass]
    public class Lucene_Text
    {
        [TestMethod]
        public void Lucene()
        {
            LuceneManage.Lucene.Add(new LuceneModel
            {
                ID = 1,
                Action = LuceneType.Insert,
                Content = "描述1",
                Title = "Title1"
            });
            LuceneManage.Lucene.Add(new LuceneModel { ID = 2, Action = LuceneType.Insert, Content = "描述2", Title = "Title2" });
            LuceneManage.Lucene.Add(new LuceneModel { ID = 3, Action = LuceneType.Insert, Content = "描述3", Title = "Title3" });
            LuceneManage.Lucene.Add(new LuceneModel { ID = 4, Action = LuceneType.Insert, Content = "描述4", Title = "Title4" });
            LuceneManage.Lucene.Add(new LuceneModel { ID = 5, Action = LuceneType.Insert, Content = "描述5", Title = "Title5" });
            LuceneManage.Lucene.StartNewThread();
            var data=LuceneManage.Lucene.SearchFromIndexData("1");
        }
    }

    public class LuceneManage
    {
        public static readonly LuceneManage Lucene=new LuceneManage();
        public static readonly string LucenePath = Environment.CurrentDirectory+"/Lucene/IndexData";
        private LuceneManage(){}

        private readonly Queue<LuceneModel> _luceneModels=new Queue<LuceneModel>();

        public void Add(LuceneModel model)
        {
            model.Action = LuceneType.Insert;
            _luceneModels.Enqueue(model);
        }

        public void Delete(int id,int type)
        {
            var model = new LuceneModel {ID = id, Type = type};
             model.Action = LuceneType.Delete;
            _luceneModels.Enqueue(model);
        }

        public void Edit(LuceneModel model)
        {
            model.Action = LuceneType.Modify;
            _luceneModels.Enqueue(model);
        }

        public void StartNewThread()
        {
            //ThreadPool.QueueUserWorkItem(QueueToLucene);
            QueueToLucene(null);
        }

        public void QueueToLucene(object para)
        {
            while (true)
            {
                OperationIndex();
                //if (_luceneModels.Count > 0)
                //{
                //    OperationIndex();
                //}
                //else
                //{
                //    Thread.Sleep(3000);
                //}
                break;
            }
        }

        public void OperationIndex()
        {
            var directory = FSDirectory.Open(new DirectoryInfo(LucenePath), new NativeFSLockFactory());
            //IndexReader:对索引库进行读取的类
            var isExist = IndexReader.IndexExists(directory);//是否存在索引库文件夹以及索引库特征文件
            if (isExist)
            {
                //如果索引目录被锁定（比如索引过程中程序异常退出或另一进程在操作索引库），则解锁
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            //创建向索引库写操作对象  IndexWriter(索引目录,指定使用盘古分词进行切词,最大写入长度限制)
            //补充:使用IndexWriter打开directory时会自动对索引库文件上锁
            var writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
            //--------------------------------遍历数据源 将数据转换成为文档对象 存入索引库
            while (_luceneModels.Count>0)
            {
                var document = new Document();//new一篇文档对象 --一条记录对应索引库中的一个文档
                var model = _luceneModels.Dequeue();
                if (model.Action == LuceneType.Insert)
                {
                    //向文档中添加字段  Add(字段,值,是否保存字段原始值,是否针对该列创建索引)
                    //--所有字段的值都将以字符串类型保存 因为索引库只存储字符串类型数据
                    document.Add(new Field("id", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    //Field.Store:表示是否保存字段原值。指定Field.Store.YES的字段在检索时才能用document.Get取出原值  
                    //Field.Index.NOT_ANALYZED:指定不按照分词后的结果保存--是否按分词后结果保存取决于是否对该列内容进行模糊查询
                    document.Add(new Field("type", model.Type.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("title", model.Title, Field.Store.YES, Field.Index.ANALYZED,
                        Field.TermVector.WITH_POSITIONS_OFFSETS));
                    //Field.Index.ANALYZED:指定文章内容按照分词后结果保存 否则无法实现后续的模糊查询 
                    //WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词 还保存词之间的距离
                    document.Add(new Field("content", model.Content, Field.Store.YES, Field.Index.ANALYZED,
                        Field.TermVector.WITH_POSITIONS_OFFSETS));
                    writer.AddDocument(document);//文档写入索引库
                }
                else if (model.Action == LuceneType.Delete)
                {
                    writer.DeleteDocuments(new Term("id", model.ID.ToString()));
                }
                else if (model.Action == LuceneType.Modify)
                {
                    //先删除 再新增
                    writer.DeleteDocuments(new Term("id", model.ID.ToString()));
                    document.Add(new Field("id", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("type", model.Type.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("title", model.Title, Field.Store.YES, Field.Index.ANALYZED,
                        Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("content", model.Content, Field.Store.YES, Field.Index.ANALYZED,
                        Field.TermVector.WITH_POSITIONS_OFFSETS));
                    writer.AddDocument(document);
                }
            }
            writer.Close();//会自动解锁
            directory.Close(); //不要忘了Close，否则索引结果搜不到
        }

        public List<LuceneModel> SearchFromIndexData(string q)
        {
            string keyword = q;
            var directory = FSDirectory.Open(new DirectoryInfo(LucenePath), new NoLockFactory());
            var reader = IndexReader.Open(directory, true);
            var searcher = new IndexSearcher(reader);

            ////--------------------------------------这里配置搜索条件
            ////PhraseQuery query = new PhraseQuery();
            ////foreach(string word in Common.SplitContent.SplitWords(Request.QueryString["SearchKey"])) {
            ////    query.Add(new Term("content", word));//这里是 and关系
            ////}
            ////query.SetSlop(100);

            ////关键词Or关系设置
            //var queryOr = new BooleanQuery();
            //TermQuery query;
            //foreach (var word in SplitContent.SplitWords(keyword))
            //{
            //    query = new TermQuery(new Term("title", word));
            //    queryOr.Add(query, BooleanClause.Occur.SHOULD);//这里设置 条件为Or关系
            //}
            ////--------------------------------------
            //var collector = TopScoreDocCollector.create(1000, true);
            ////searcher.Search(query, null, collector);
            //searcher.Search(queryOr, null, collector);

            q = SplitContent.SplitWords(q, new PanGuTokenizer());
            var queryParser = new QueryParser("content", new PanGuAnalyzer(true));
            var query = queryParser.Parse(q);
            var titleQueryParser = new QueryParser("title", new PanGuAnalyzer(true));
            var titleQuery = titleQueryParser.Parse(q);
            var bq = new BooleanQuery();
            bq.Add(query, BooleanClause.Occur.SHOULD);
            bq.Add(titleQuery, BooleanClause.Occur.SHOULD);
            var collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(bq, null, collector);
            var totalCount = collector.GetTotalHits();//返回总条数
            var docs = collector.TopDocs(0, 10).scoreDocs;//取前十条数据  可以通过它实现LuceneNet搜索结果分页
            var result = new List<LuceneModel>();
            for (int i = 0; i < docs.Length; i++)
            {
                var docId = docs[i].doc;
                var doc = searcher.Doc(docId);
                var model = new LuceneModel();
                model.Content = doc.Get("content");
                model.HightLightContent = SplitContent.HightLight(keyword, doc.Get("content"));
                model.Title = doc.Get("title");
                model.HightLightTitle = SplitContent.HightLight(keyword, doc.Get("title"));
                model.ID = Convert.ToInt32(doc.Get("id"));
                result.Add(model);
            }
            return result;
        }
    }

    public class LuceneModel
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string HightLightTitle { get; set; }
        public string Content { get; set; }
        public string HightLightContent { get; set; }
        public LuceneType Action { get; set; }
    }

    public enum LuceneType
    {
        Insert,
        Modify,
        Delete
    }
}
