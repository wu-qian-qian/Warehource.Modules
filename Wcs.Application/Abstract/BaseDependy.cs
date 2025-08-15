using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Application.Abstract
{
    public abstract class BaseDependy
    {
        protected readonly IServiceScopeFactory _scopeFactory;

        protected BaseDependy(IServiceScopeFactory serviceScopeFactory)
        {
            _scopeFactory = serviceScopeFactory;
        }
    }
}