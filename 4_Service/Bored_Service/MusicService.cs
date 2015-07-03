using System;
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
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicDal;
        private readonly IConfigInfoRepository _configInfoDal;

        public MusicService(IMusicRepository musicDal, IConfigInfoRepository configInfoDal)
        {
            _musicDal = musicDal;
            _configInfoDal = configInfoDal;
        }

        private readonly string _rootpath = string.Format("/Upload/Music/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        public int Add(MusicDto model)
        {
            var entity = Mapper.Map<Music>(model);
            entity.CreateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Image))
            {
                entity.Image = BllHelper.RemoveImg(_rootpath, entity.Image);
                model.Image = entity.Image;
            }
            var id = _musicDal.Insert(entity);
            if (id > 0)
            {
                SetLucene(entity, LuceneType.Insert);
            }
            return id;
        }

        public MusicDto GetModel(int id)
        {
            var data = _musicDal.GetModel(t => t.ID == id);
            return Mapper.Map<MusicDto>(data);
        }

        public PageData GetPage(int page, int rows, bool? isMv, string song = "", string songer = "")
        {
            return _musicDal.GetViewPage(page, rows, isMv, song, songer);
        }

        public bool Update(MusicDto model)
        {
            var oldModel = GetModel(model.ID);
            if (oldModel.Image != model.Image)
            {
                model.Image = BllHelper.RemoveImg(_rootpath, model.Image, oldModel.Image);
            }
            else
                model.Image = oldModel.Image;
            model.UserId = oldModel.UserId;
            model.CreateTime = oldModel.CreateTime;
            model.IsDel = oldModel.IsDel;
            var entity = Mapper.Map<Music>(model);
            var result = _musicDal.Update(entity);
            if (result)
                SetLucene(entity, LuceneType.Modify);
            return result;
        }

        public bool Delete(string id)
        {
            var idList = id.Split(',');
            var result = _musicDal.Update(t => idList.Contains(t.ID.ToString()), t => new Music { IsDel = true });
            if (result)
            {
                foreach (var item in idList)
                {
                    SetLucene(new Music { ID = Convert.ToInt32(item) }, LuceneType.Delete);
                }
            }
            return result;
        }

        /// <summary>
        /// 操作索引
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        private void SetLucene(Music model, LuceneType type)
        {
            switch (type)
            {
                case LuceneType.Insert:
                    LuceneManager.Lucene.Add(new LuceneModel
                    {
                        ID = model.ID,
                        IndexType = LuceneType.Insert,
                        Title = model.Song,
                        Content = model.Songer,
                        Images = model.Image,
                        ClickCount = model.ClickCount,
                        CreateTime = DateTime.Now,
                        Tags = _configInfoDal.GetConfigNames(model.Tags),
                        Type = (int)IndexEnum.Game
                    });
                    break;
                case LuceneType.Modify:
                    LuceneManager.Lucene.Edit(new LuceneModel
                    {
                        ID = model.ID,
                        IndexType = LuceneType.Insert,
                        Title = model.Song,
                        Content = model.Songer,
                        Images = model.Image,
                        ClickCount = model.ClickCount,
                        CreateTime = model.CreateTime,
                        Tags = _configInfoDal.GetConfigNames(model.Tags),
                        Type = (int)IndexEnum.Game
                    });
                    break;
                case LuceneType.Delete:
                    LuceneManager.Lucene.Delete(model.ID, (int)IndexEnum.Game);
                    break;
            }
        }


        public void UpdateLucene<T>(T model,int type) where T : Music
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
                            Title = model.Song,
                            Content = model.Songer,
                            Images = model.Image,
                            ClickCount = model.ClickCount,
                            CreateTime = DateTime.Now,
                            Tags = _configInfoDal.GetConfigNames(model.Tags),
                            Type = (int)IndexEnum.Game
                        });
                        break;
                    case (int)LuceneType.Modify:
                        LuceneManager.Lucene.Edit(new LuceneModel
                        {
                            ID = model.ID,
                            IndexType = LuceneType.Insert,
                            Title = model.Song,
                            Content = model.Songer,
                            Images = model.Image,
                            ClickCount = model.ClickCount,
                            CreateTime = model.CreateTime,
                            Tags = _configInfoDal.GetConfigNames(model.Tags),
                            Type = (int)IndexEnum.Game
                        });
                        break;
                    case (int)LuceneType.Delete:
                        LuceneManager.Lucene.Delete(model.ID, (int)IndexEnum.Game);
                        break;
                }
            });
        }
    }
}
