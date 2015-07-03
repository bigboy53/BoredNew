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
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoDal;
        private readonly IConfigInfoRepository _configInfoDal;

        public VideoService(IVideoRepository videoDal, IConfigInfoRepository configInfoDal)
        {
            _videoDal = videoDal;
            _configInfoDal = configInfoDal;
        }

        private readonly string _rootpath = string.Format("/Upload/Video/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        public int Add(VideoDto model)
        {
            var entity = Mapper.Map<Video>(model);
            entity.CreateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Image))
            {
                entity.Image = BllHelper.RemoveImg(_rootpath, entity.Image);
                model.Image = entity.Image;
            }
            var id = _videoDal.Insert(entity);
            if (id > 0)
            {
                UpdateLucene(entity, (int)LuceneType.Insert);
            }
            return id;
        }

        public VideoDto GetModel(int id)
        {
            var data = _videoDal.GetModel(t => t.ID == id);
            return Mapper.Map<VideoDto>(data);
        }

        public PageData GetPage(int page, int rows, string title, int? source)
        {
            var data = _videoDal.GetViewPage(page, rows, title, source);
            var config = _configInfoDal.GetAllList();
            foreach (var item in (List<VideoDto>)data.Data)
            {
                var configModel = config.FirstOrDefault(t => t.ID == Convert.ToInt32(item.Source));
                if (configModel != null)
                    item.SourceTxt = configModel.Name;
            }
            return data;
        }

        public bool Update(VideoDto model)
        {
            var oldModel = GetModel(model.ID);
            if (oldModel.Image != model.Image)
            {
                model.Image = BllHelper.RemoveImg(_rootpath, model.Image, oldModel.Image);
            }
            model.UserId = oldModel.UserId;
            model.CreateTime = oldModel.CreateTime;
            model.IsDel = oldModel.IsDel;
            var entity = Mapper.Map<Video>(model);
            var result = _videoDal.Update(entity);
            if (result)
                UpdateLucene(entity, (int)LuceneType.Modify);
            return result;
        }

        public bool Delete(string id)
        {
            var idList = id.Split(',');
            var result = _videoDal.Update(t => idList.Contains(t.ID.ToString()), t => new Video { IsDel = true });
            if (result)
            {
                foreach (var item in idList)
                {
                    UpdateLucene(new Video { ID = Convert.ToInt32(item) }, (int)LuceneType.Delete);
                }
            }
            return result;
        }

        /// <summary>
        /// 操作索引
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        public void UpdateLucene<T>(T model, int type) where T : Video
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
                            Title = model.Title,
                            Content = model.Description,
                            Images = model.Image,
                            ClickCount = model.ClickCount,
                            CreateTime = DateTime.Now,
                            Tags = _configInfoDal.GetConfigNames(model.Tags),
                            Type = (int) IndexEnum.Game
                        });
                        break;
                    case (int)LuceneType.Modify:
                        LuceneManager.Lucene.Edit(new LuceneModel
                        {
                            ID = model.ID,
                            IndexType = LuceneType.Insert,
                            Title = model.Title,
                            Content = model.Description,
                            Images = model.Image,
                            ClickCount = model.ClickCount,
                            CreateTime = model.CreateTime,
                            Tags = _configInfoDal.GetConfigNames(model.Tags),
                            Type = (int) IndexEnum.Game
                        });
                        break;
                    case (int)LuceneType.Delete:
                        LuceneManager.Lucene.Delete(model.ID, (int) IndexEnum.Game);
                        break;
                }
            });
        }
    }
}
