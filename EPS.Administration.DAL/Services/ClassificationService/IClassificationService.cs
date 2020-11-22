using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.ClassificationService
{
    public interface IClassificationService
    {
        void AddOrUpdate(Classification classification);
        void AddOrUpdate(IEnumerable<Classification> classifications);
        /// <summary>
        /// Gets group by group code
        /// </summary>
        /// <param name="code">group code</param>
        /// <returns></returns>
        Classification Get(string code);
        /// <summary>
        /// Gets group by ID
        /// </summary>
        /// <param name="id">group id</param>
        /// <returns></returns>
        Classification Get(int id);
        List<Classification> Get();
    }
}
