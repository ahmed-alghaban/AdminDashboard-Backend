using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Utilities
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}