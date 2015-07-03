using System;
using System.Threading.Tasks;
using AutoMapper;
using Bored.Model;
using Bored.IService;
using Bored.IRepository;
using DKD.Core.Lucene;
using DKD.Framework.Contract.Enum;
using DKD.Framework.Data.Infrastructure;
using Manage.ViewModel;
using PageHelper;
using System.Linq;

namespace Bored.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameDal;
        private readonly IConfigInfoRepository _configInfoDal;

        public GameService(IGameRepository gameDal, IConfigInfoRepository configInfoDal)
        {
            _gameDal = gameDal;
            _configInfoDal = configInfoDal;
        }

        private readonly string _rootpath = string.Format("/Upload/Game/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        public int Add(GameDto model)
        {
            var entity = Mapper.Map<Game>(model);
            entity.CreateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Image))
            {
                entity.Image = BllHelper.RemoveImg(_rootpath, entity.Image);
                model.Image = entity.Image;
            }
            var id = _gameDal.Insert(entity);
            if (id > 0)
            {
                UpdateLucene(entity, (int)LuceneType.Insert);
            }
            return id;
        }

        public GameDto GetModel(int id)
        {
            var data = _gameDal.GetModel(t => t.ID == id);
            return Mapper.Map<GameDto>(data);
        }

        public PageData GetPage(int page, int rows, string title)
        {
            return _gameDal.GetViewPage(page, rows, title);
        }

        public bool Update(GameDto model)
        {
            var oldModel = GetModel(model.ID);
            if (oldModel.Image != model.Image)
            {
                model.Image = BllHelper.RemoveImg(_rootpath, model.Image, oldModel.Image);
            }
            model.UserId = oldModel.UserId;
            model.CreateTime = oldModel.CreateTime;
            model.IsDel = oldModel.IsDel;
            var entity = Mapper.Map<Game>(model);
            var result = _gameDal.Update(entity);
            if (result)
                UpdateLucene(entity, (int)LuceneType.Modify);
            return result;
        }

        public bool Delete(string id)
        {
            var idList = id.Split(',');
            var result = _gameDal.Update(t => idList.Contains(t.ID.ToString()), t => new Game { IsDel = true });
            if (result)
            {
                foreach (var item in idList)
                {
                    UpdateLucene(new Game { ID = Convert.ToInt32(item) }, (int)LuceneType.Delete);
                }
            }
            return result;
        }
        /// <summary>
        /// 操作索引
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        public void UpdateLucene<T>(T model, int type) where T : Game
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
                            Title = model.Title,
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
