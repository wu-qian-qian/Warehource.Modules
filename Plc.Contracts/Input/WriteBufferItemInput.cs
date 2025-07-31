using Plc.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Contracts.Input
{
    public class WriteBufferItemInput
    {
        public object Value { get; set; }

        /// <summary>
        /// db地址
        /// </summary>
        public int DBAddress { get; set; }

        /// <summary>
        /// bit地址
        /// </summary>
        public byte DBBit { get; set; }
 
        /// <summary>
        /// 起始地址
        /// </summary>
        public int DBStart { get; set; }

        /// <summary>
        /// 缓冲区
        /// </summary>
        public byte[] Buffer { get; set; }
        
        public S7BlockTypeEnum S7BlockType { get; set; }

        public S7DataTypeEnum S7DataType { get; set; }
    }
}
