using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.ClassificationService
{
    public interface IClassificationService
    {
        void AddOrUpdate(Classification classification);
        void AddOrUpdate(IEnumerable<Classification> classifications);
        Classification Get(string code);
        ClassificationData ToDTO(Classification classification);
    }
}
