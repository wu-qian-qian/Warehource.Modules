using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Application.Handler.Execute.WritePlcTaskData
{
    public class WritePlcTaskdataCommand:ICommand
    {
        public string Key { get;set; }
    }
}
