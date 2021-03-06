﻿using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DetailedStatusService
{
    public interface IDetailedStatusService
    {
        void AddOrUpdate(DetailedStatus status);
        void AddOrUpdate(IEnumerable<DetailedStatus> statuses);
        DetailedStatus Get(string status);
        List<DetailedStatus> Get();
        DetailedStatus Get(int id);
    }
}
