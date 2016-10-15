using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Swiss.Core
{
    public class DisposeCallback : IDisposable
    {
        public Action OnDispose { get; set; }
        public void Dispose()
        {
            OnDispose();
        }
    }
}
