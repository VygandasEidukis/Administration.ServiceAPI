using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Exceptions
{
    public class AdministrationException : Exception
    {
        public AdministrationException(string message) : base(message) { }
    }
}
