using ErrorOr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions;

public class DupplicateProjectNumberException : Exception
{
    public DupplicateProjectNumberException(string? message) : base(message)
    {
    }

    public DupplicateProjectNumberException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

