using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.MediatR.Behaviors
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get;set; }

        public string Message { get; set; }

        public void SetValue(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        public void SetMessage(string value)
        {
            Message = value;
            IsSuccess = false;
        }
    }
}
