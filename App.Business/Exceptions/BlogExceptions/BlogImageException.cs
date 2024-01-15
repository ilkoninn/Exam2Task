using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions.BlogExceptions
{
    public class BlogImageException : Exception
    {
        public string ParamName { get; set; }
        public BlogImageException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
