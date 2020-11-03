using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.DAL.Data
{
    public interface IRevisionableEntity
    {
        int Id { get; set; }
        int Revision { get; set; }
    }
}
