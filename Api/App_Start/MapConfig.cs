using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;

namespace Api
{
    public class MapConfig : MapperConfiguration
    {
        public MapConfig(MapperConfigurationExpression configurationExpression) : base(configurationExpression)
        {
        }

        public MapConfig(Action<IMapperConfigurationExpression> configure) : base(configure)
        {
        }
    }
}