using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shared
{
    public class PagingQuery:IRequest
    {
      
        /// <summary>
        /// 数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 跳过数量
        /// </summary>
        public int SkipCount => (PageIndex - 1) * Total;
    }
}
