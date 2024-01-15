using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions.BlogExceptions
{
    public class BlogNotFoundException : Exception
    {
        public string ParamName { get; set; }
        public BlogNotFoundException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
