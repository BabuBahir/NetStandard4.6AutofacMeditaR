using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample4._6Api.Models
{
    public interface ILogger
    {
        void Write(string message, params object[] args);
    }
}