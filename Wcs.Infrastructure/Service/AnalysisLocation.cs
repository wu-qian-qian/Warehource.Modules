using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;

namespace Wcs.Infrastructure.Service
{
    internal class AnalysisLocation : IAnalysisLocation
    {
        public string[] Analysis(string location)
        {
            //解析地址格式 1_1_1_1_1  做一些WCS专属解析
            var locations = location.Split('_', StringSplitOptions.RemoveEmptyEntries);
            //上游系统地址格式 1_1_1
            if (locations.Length == 3)
            {
                //TODO  根据上游储位规则进行解析
            }
            else if (locations.Length == 5)
            {
                
            }
            else
            {
                throw new Exception("地址格式错误");
            }
            return locations;
        }

        public GetLocation AnalysisGetLocation(string location)
        {
            var locations = Analysis(location);
            if(locations.Length!=5) throw new AggregateException("地址格式错误");
            GetLocation getLocation = new 
                GetLocation(locations[0], locations[3], locations[1], locations[2], locations[4]);
            return getLocation;
        }

        public PutLocation AnalysisPutLocation(string location)
        {
            var locations = Analysis(location);
            if (locations.Length != 5) throw new AggregateException("地址格式错误");
            PutLocation putLocation = new
                PutLocation(locations[0], locations[3], locations[1], locations[2], locations[4]);
            return putLocation;
        }

        public bool CanApplyGetLocation(GetLocation location)
        {
            if (location.GetTunnel != null || location.GetFloor != null ||
               location.GetRow != null ||
               location.GetColumn != null)
            {
                return true;
            }
            return false;
        }

        public bool CanApplyPutLocation(PutLocation location)
        {
            if (location.PutDepth != null || location.PutTunnel != null ||
               location.PutFloor != null ||
               location.PutRow != null ||
               location.PutColumn != null)
            {
                return true;
            }
            return false;
        }
    }
}
