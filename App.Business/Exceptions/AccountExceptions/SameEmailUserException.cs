﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions.AccountExceptions
{
    public class SameEmailUserException : Exception
    {
        public string ParamName { get; set; }
        public SameEmailUserException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
