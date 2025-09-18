using Common.Shared;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.MediatR.Message.PageQuery
{
    public interface IPageHandler<in TQuery, TResponse> : IRequestHandler<TQuery, PageResult<TResponse>>
    where TQuery : PageQuery<TResponse>;
}
