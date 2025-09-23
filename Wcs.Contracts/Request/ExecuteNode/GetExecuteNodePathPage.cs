using Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Shared;

namespace Wcs.Contracts.Request.ExecuteNode
{
    public class GetExecuteNodePathPage : PagingQuery
    {
        /// <summary>
        ///     路径组
        /// </summary>
        public string? PahtNodeGroup { get; set; }

        /// <summary>
        ///     任务类型
        /// </summary>
        public WcsTaskTypeEnum? TaskType { get; set; }
    }
}