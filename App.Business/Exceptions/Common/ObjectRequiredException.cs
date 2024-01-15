using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions.Common
{
    public class ObjectRequiredException : Exception
    {
        public string ParamName { get; set; }
        public ObjectRequiredException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
