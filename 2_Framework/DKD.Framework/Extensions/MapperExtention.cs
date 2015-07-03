using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace DKD.Framework.Extensions
{
    public static class MapperExtention
    {
        /// <summary>
        /// 对象对对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T MapTo<T>(this object self)
        {
            if (self == null)
                throw new ArgumentNullException();
            //Mapper.CreateMap(self.GetType(), typeof (T));//放在global里面
            return (T) Mapper.Map(self, self.GetType(), typeof (T));
        }
        /// <summary>
        /// 集合对集合
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
                throw new ArgumentNullException();
            //Mapper.CreateMap(self.GetType(), typeof(TResult));//放在global里面
            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }
    }
}
