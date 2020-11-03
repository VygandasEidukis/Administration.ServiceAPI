using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DetailedStatusService
{
    public interface IDetailedStatusService
    {
        void AddOrUpdate(DetailedStatus status);
        void AddOrUpdate(IEnumerable<DetailedStatus> statuses);
    }
}
