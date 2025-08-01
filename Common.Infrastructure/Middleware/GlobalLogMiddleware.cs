using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Middleware
{
    public class GlobalLogMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    }
}
