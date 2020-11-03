using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.ClassificationService
{
    public interface IClassificationService
    {
        void AddOrUpdate(Classification status);
        void AddOrUpdate(IEnumerable<Classification> statuses);
    }
}
