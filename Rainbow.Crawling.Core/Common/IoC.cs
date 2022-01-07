using System;
using System.Collections.Generic;
using System.Text;
using Rainbow.Crawling.Core.Configurations;
using StructureMap;

namespace Rainbow.Crawling.Core.Common
{
    public class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<CrawlingRegistry>();
            });

            return ObjectFactory.Container;
        }
    }
}
