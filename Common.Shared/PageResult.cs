using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shared
{
    public class PageResult<T>
    {
        /// <summary>
        /// 页面数据
        /// </summary>
        public IReadOnlyList<T> Data { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public long TotalCount { get; set; }

        public PageResult(long totalCount, IReadOnlyList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}
