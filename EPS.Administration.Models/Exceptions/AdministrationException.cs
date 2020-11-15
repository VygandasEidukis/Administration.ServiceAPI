using System;

namespace EPS.Administration.Models.Exceptions
{
    public class AdministrationException : Exception
    {
        public AdministrationException(string message) : base(message)
        {
        }
    }
}