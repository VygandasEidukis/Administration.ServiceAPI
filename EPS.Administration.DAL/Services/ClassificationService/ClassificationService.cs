﻿using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.DAL.Services.ClassificationService
{
    public class ClassificationService : IClassificationService
    {
        private readonly IBaseService<ClassificationData> _classificationService;

        public ClassificationService(IBaseService<ClassificationData> baseService)
        {
            _classificationService = baseService;
        }

        public void AddOrUpdate(Classification classification)
        {
            _classificationService.AddOrUpdate(ToDTO(classification));
        }

        public void AddOrUpdate(IEnumerable<Classification> statuses)
        {
            var dtos = statuses.Select(x => ToDTO(x));
            foreach (var dto in dtos)
            {
                var item = _classificationService.GetSingle(x => x.Code == dto.Code);
                dto.Id = item == null ? 0 : item.Id;
                _classificationService.AddOrUpdate(dto);
            }
            _classificationService.Save();
        }

        private ClassificationData ToDTO(Classification classific)
        {
            if (classific == null)
            {
                return null;
            }

            var dataClassification = new ClassificationData()
            {
                Id = classific.Id,
                Revision = classific.Revision,
                Code = classific.Code,
                Model = classific.Model,
            };

            return dataClassification;
        }
    }
}