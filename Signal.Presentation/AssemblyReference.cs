using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Presentation
{
    internal class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

        public static readonly string Tasgs = "SignalR";
    }
}
