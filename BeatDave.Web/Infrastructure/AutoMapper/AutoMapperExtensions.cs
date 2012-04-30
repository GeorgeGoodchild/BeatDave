using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace BeatDave.Web.Infrastructure
{
    public static class AutoMapperExtensions
    {
        public static TMapTo MapTo<TMapTo>(this object mapFrom)
        {
            if (mapFrom == null)
                throw new ArgumentNullException();

            return (TMapTo)Mapper.Map(mapFrom, mapFrom.GetType(), typeof(TMapTo));
        }

        public static TMapTo MapToInstance<TMapTo>(this object mapFrom, TMapTo mapTo)
        {
            if (mapFrom == null)
                throw new ArgumentNullException();

            return (TMapTo)Mapper.Map(mapFrom, mapTo, mapFrom.GetType(), typeof(TMapTo));
        }
    }
}