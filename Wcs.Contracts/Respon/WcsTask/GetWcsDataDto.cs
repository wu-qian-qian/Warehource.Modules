using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Contracts.Respon.WcsTask
{
    public class GetWcsDataDto
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string Name { get;set; }
        /// <summary>
        /// 完成数量
        /// </summary>
        public int Value { get; set; } 
        /// <summary>
        /// 未完成数量
        /// </summary>
        public int Growth { get; set; }
    }
}
