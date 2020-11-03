﻿using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPS.Administration.DAL.Services.DetailedStatusService
{
    public class DetailedStatusService : IDetailedStatusService
    {
        private readonly IBaseService<DetailedStatusData> _detailedStatusService;

        public DetailedStatusService(IBaseService<DetailedStatusData> baseService)
        {
            _detailedStatusService = baseService;
        }

        public void AddOrUpdate(DetailedStatus status)
        {
            _detailedStatusService.AddOrUpdate(ToDTO(status));
            _detailedStatusService.Save();
        }

        public void AddOrUpdate(IEnumerable<DetailedStatus> statuses)
        {
            var dtos = statuses.Select(x => ToDTO(x));
            foreach(var dto in dtos)
            {
                var item = _detailedStatusService.GetSingle(x=>x.Status == dto.Status);
                dto.Id = item == null ? 0 : item.Id;
                _detailedStatusService.AddOrUpdate(dto);
            }
            _detailedStatusService.Save();
        }

        private DetailedStatusData ToDTO(DetailedStatus status)
        {
            if(status == null)
            {
                return null;
            }

            var dataStatus = new DetailedStatusData()
            {
                Id = status.Id,
                Revision = status.Revision,
                Description = status.Description,
                Status = status.Status,
            };

            return dataStatus;
        }
    }
}