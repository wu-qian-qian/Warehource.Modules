using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Plc.Application.Handler.DataBase.Delete
{
    public class DeletePlcCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}