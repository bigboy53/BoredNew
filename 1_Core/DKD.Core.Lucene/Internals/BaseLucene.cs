using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Web;
using DKD.Framework.Extensions;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace DKD.Core.Lucene
{
    public abstract class BaseLucene
    {
        public static string LucenePath { get; private set; }

        static BaseLucene()
        {
            LucenePath = HttpContext.Current.Server.MapPath("/Lucene/IndexData");
        }
        
        public ConcurrentQueue<LuceneModel> LuceneModels = new ConcurrentQueue<LuceneModel>();

        protected virtual void QueueToLucene(object para)
        {
            while (true)
            {
                if (LuceneModels.Count > 0)
                {
                    OperationIndex();
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        protected virtual void OperationIndex()
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
            while (!LuceneModels.IsEmpty)
            {
                LuceneModel model;
                LuceneModels.TryDequeue(out model);
                if(model==null||model.ID<=0)
                    continue;
                if (model.IndexType == LuceneType.Insert)
                {
                    InsertData(model,writer);
                }
                else if (model.IndexType == LuceneType.Delete)
                {
                    DeleteData(model,writer);
                }
                else if (model.IndexType == LuceneType.Modify)
                {
                    ModifyData(model,writer);
                }
            }

            #region 非线程安全
            //while (LuceneModels.Count > 0)
            //{
            //    var document = new Document();//new一篇文档对象 --一条记录对应索引库中的一个文档
            //    var model = LuceneModels.Dequeue();
            //    if (model.IndexType == LuceneType.Insert)
            //    {
            //        //向文档中添加字段  Add(字段,值,是否保存字段原始值,是否针对该列创建索引)
            //        //--所有字段的值都将以字符串类型保存 因为索引库只存储字符串类型数据
            //        document.Add(new Field("id", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //        //Field.Store:表示是否保存字段原值。指定Field.Store.YES的字段在检索时才能用document.Get取出原值  
            //        //Field.Index.NOT_ANALYZED:指定不按照分词后的结果保存--是否按分词后结果保存取决于是否对该列内容进行模糊查询
            //        document.Add(new NumericField("type", Field.Store.YES, true).SetIntValue(model.Type));
            //        document.Add(new Field("title", model.Title, Field.Store.YES, Field.Index.ANALYZED,
            //            Field.TermVector.WITH_POSITIONS_OFFSETS));
            //        //Field.Index.ANALYZED:指定文章内容按照分词后结果保存 否则无法实现后续的模糊查询 
            //        //WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词 还保存词之间的距离
            //        if (!string.IsNullOrEmpty(model.Content))
            //            document.Add(new Field("content", model.Content, Field.Store.YES, Field.Index.ANALYZED,
            //                Field.TermVector.WITH_POSITIONS_OFFSETS));
            //        document.Add(
            //            new NumericField("createtime", Field.Store.YES, true).SetIntValue(
            //                DateTimeExtension.DateTimeToUnix(model.CreateTime)));
            //        if (!string.IsNullOrEmpty(model.Images))
            //            document.Add(new Field("images", model.Images, Field.Store.YES, Field.Index.NO));
            //        if (!string.IsNullOrEmpty(model.Tags))
            //            document.Add(new Field("tags", model.Tags, Field.Store.YES, Field.Index.ANALYZED,
            //                Field.TermVector.WITH_POSITIONS_OFFSETS));
            //        document.Add(new NumericField("clickcount", Field.Store.YES, true).SetIntValue(model.ClickCount));
            //        writer.AddDocument(document);//文档写入索引库
            //    }
            //    else if (model.IndexType == LuceneType.Delete)
            //    {
            //        writer.DeleteDocuments(new[] { new Term("id", model.ID.ToString()), new Term("type", model.Type.ToString()) });
            //    }
            //    else if (model.IndexType == LuceneType.Modify)
            //    {
            //        //先删除 再新增
            //        writer.DeleteDocuments(new[] { new Term("id", model.ID.ToString()), new Term("type", model.Type.ToString()) });
            //        document.Add(new Field("id", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //        document.Add(new NumericField("type", Field.Store.YES, true).SetIntValue(model.Type));
            //        document.Add(new Field("title", model.Title, Field.Store.YES, Field.Index.ANALYZED,
            //            Field.TermVector.WITH_POSITIONS_OFFSETS));
            //        if (!string.IsNullOrEmpty(model.Content))
            //            document.Add(new Field("content", model.Content, Field.Store.YES, Field.Index.ANALYZED,
            //                Field.TermVector.WITH_POSITIONS_OFFSETS));
            //        document.Add(
            //            new NumericField("createtime", Field.Store.YES, true).SetIntValue(
            //                DateTimeExtension.DateTimeToUnix(model.CreateTime)));
            //        if (!string.IsNullOrEmpty(model.Images))
            //            document.Add(new Field("images", model.Images, Field.Store.YES, Field.Index.NO));
            //        if (!string.IsNullOrEmpty(model.Tags))
            //            document.Add(new Field("tags", model.Tags, Field.Store.YES, Field.Index.ANALYZED,
            //                Field.TermVector.WITH_POSITIONS_OFFSETS));
            //        document.Add(new NumericField("clickcount", Field.Store.YES, true).SetIntValue(model.ClickCount));
            //        writer.AddDocument(document);
            //    }
            //}
            #endregion

            writer.Close();//会自动解锁
            directory.Close(); //不要忘了Close，否则索引结果搜不到
        }

        #region 操作
        private void InsertData(LuceneModel model, IndexWriter writer)
        {
            var document = new Document();//new一篇文档对象 --一条记录对应索引库中的一个文档

            //向文档中添加字段  Add(字段,值,是否保存字段原始值,是否针对该列创建索引)
            //--所有字段的值都将以字符串类型保存 因为索引库只存储字符串类型数据
            document.Add(new Field("id", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //Field.Store:表示是否保存字段原值。指定Field.Store.YES的字段在检索时才能用document.Get取出原值  
            //Field.Index.NOT_ANALYZED:指定不按照分词后的结果保存--是否按分词后结果保存取决于是否对该列内容进行模糊查询
            document.Add(new NumericField("type", Field.Store.YES, true).SetIntValue(model.Type));
            document.Add(new Field("title", model.Title, Field.Store.YES, Field.Index.ANALYZED,
                Field.TermVector.WITH_POSITIONS_OFFSETS));
            //Field.Index.ANALYZED:指定文章内容按照分词后结果保存 否则无法实现后续的模糊查询 
            //WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词 还保存词之间的距离
            if (!string.IsNullOrEmpty(model.Content))
                document.Add(new Field("content", model.Content, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
            document.Add(
                new NumericField("createtime", Field.Store.YES, true).SetIntValue(
                    DateTimeExtension.DateTimeToUnix(model.CreateTime)));
            if (!string.IsNullOrEmpty(model.Images))
                document.Add(new Field("images", model.Images, Field.Store.YES, Field.Index.NO));
            if (!string.IsNullOrEmpty(model.Tags))
                document.Add(new Field("tags", model.Tags, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
            document.Add(new NumericField("clickcount", Field.Store.YES, true).SetIntValue(model.ClickCount));
            writer.AddDocument(document);
        }

        private void DeleteData(LuceneModel model, IndexWriter writer)
        {
            writer.DeleteDocuments(new[] {new Term("id", model.ID.ToString()), new Term("type", model.Type.ToString())});
        }

        private void ModifyData(LuceneModel model, IndexWriter writer)
        {
            DeleteData(model, writer);
            InsertData(model,writer);
        }
        #endregion

        public virtual void DeleteAll()
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
            Query query = new WildcardQuery(new Term("title", "*"));
            writer.DeleteDocuments(query);

            writer.Close();//会自动解锁
            directory.Close(); //不要忘了Close，否则索引结果搜不到
        }
    }
}
