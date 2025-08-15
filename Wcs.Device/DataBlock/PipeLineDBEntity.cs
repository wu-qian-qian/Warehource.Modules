using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device.Abstract;

namespace Wcs.Device.DataBlock
{
    public class PipeLineDBEntity : BaseDBEntity
    {
        public string RTask { get; set; }
    }
}