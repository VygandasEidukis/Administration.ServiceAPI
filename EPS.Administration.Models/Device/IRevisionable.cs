using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public interface IRevisionable
    {
        int Id { get; set; }
        int Revision { get; set; }
    }
}
