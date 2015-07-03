using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bored.Model;
using Bored.IService;
using Bored.IRepository;
using DKD.Core.Lucene;
using DKD.Framework.Contract.Enum;
using Manage.ViewModel;
using PageHelper;

namespace Bored.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleDal;
        private readonly IConfigInfoRepository _configInfoDal;

        public ArticleService(IArticleRepository articleDal, IConfigInfoRepository configInfoDal)
        {
            _articleDal = articleDal;
            _configInfoDal = configInfoDal;
        }
        private readonly string _rootpath = string.Format("/Upload/Article/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        public int Add(ArticleDto model)
        {
            var imgs = "";
            if (!string.IsNullOrEmpty(model.Image))
            {
                int index = 1;
                model.ArticleImages = model.ArticleImages ?? new List<ArticleImagesDto>();
                foreach (var item in model.Image.Split(','))
                {
                    var imgPath = BllHelper.RemoveImg(_rootpath, item);
                    if (!string.IsNullOrEmpty(imgPath))
                    {
                        model.ArticleImages.Add(new ArticleImagesDto
                        {
                            URL = imgPath,
                            Number = index
                        });
                    }
                    index++;
                }
                if (model.ArticleImages.Count > 0)
                {
                    imgs = string.Join(",", model.ArticleImages.Select(t => t.URL).ToArray());
                }
            }
            model.CreatTime = DateTime.Now;

            var id = _articleDal.Add(model);
            if (id > 0)
            {
                model.ID = id;
                UpdateLucene(Mapper.Map<Article>(model), (int)LuceneType.Insert, imgs);
            }
            return id;
        }

        public ArticleDto GetModel(int id)
        {
            var data = _articleDal.GetModel(id);
            return data;
        }

        public PageData GetPage(int page, int rows, string title, string userName, int? source)
        {
            var data = _articleDal.GetViewPage(page, rows, title, userName, source);
            var config = _configInfoDal.GetAllList();
            foreach (var item in (List<ArticleDto>)data.Data)
            {
                var configModel = config.FirstOrDefault(t => t.ID == Convert.ToInt32(item.Source));
                if (configModel != null)
                    item.SourceTxt = configModel.Name;
            }
            return data;
        }

        public bool Update(ArticleDto model)
        {
            var oldModel = GetModel(model.ID);
            model.UserId = oldModel.UserId;
            model.CreatTime = oldModel.CreatTime;
            model.IsDel = oldModel.IsDel;
            var entity = Mapper.Map<Article>(model);
            var result = _articleDal.Update(entity);
            if (result)
            {
                //不能修改图片
                var imgs="";
                if (oldModel.ArticleImages.Count > 0)
                {
                    imgs = string.Join(",", oldModel.ArticleImages.Select(t => t.URL).ToArray());
                }
                UpdateLucene(entity, (int)LuceneType.Modify, imgs);
            }
            return result;
        }

        public bool Delete(string id)
        {
            var idList = id.Split(',');
            var result = _articleDal.Update(t => idList.Contains(t.ID.ToString()), t => new Article { IsDel = true });
            if (result)
            {
                foreach (var item in idList)
                {
                    UpdateLucene(new Article { ID = Convert.ToInt32(item) }, (int)LuceneType.Delete);
                }
            }
            return result;
        }

        /// <summary>
        /// 操作索引
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public void UpdateLucene<T>(T model, int type,string image="") where T : Article
        {
            Task.Factory.StartNew(() =>
            {
                switch (type)
                {
                    case (int)LuceneType.Insert:
                        LuceneManager.Lucene.Add(new LuceneModel
                        {
                            ID = model.ID,
                            IndexType = LuceneType.Insert,
                            Content = model.Content,
                            Title = model.Content,
                            Images = image,
                            ClickCount = model.LookCount,
                            CreateTime = DateTime.Now,
                            Tags = _configInfoDal.GetConfigNames(model.Tags),
                            Type = (int)IndexEnum.Article
                        });
                        break;
                    case (int)LuceneType.Modify:
                        LuceneManager.Lucene.Edit(new LuceneModel
                        {
                            ID = model.ID,
                            IndexType = LuceneType.Insert,
                            Content = model.Content,
                            Title = model.Content,
                            Images = image,
                            ClickCount = model.LookCount,
                            CreateTime = model.CreateTime,
                            Tags = _configInfoDal.GetConfigNames(model.Tags),
                            Type = (int)IndexEnum.Article
                        });
                        break;
                    case (int)LuceneType.Delete:
                        LuceneManager.Lucene.Delete(model.ID, (int)IndexEnum.Article);
                        break;
                }
            });
        }
    }
}
