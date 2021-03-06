﻿using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;

namespace EPS.Administration.DAL.Mapping
{
    public class DeviceMapping : Profile
    {
        public DeviceMapping()
        {
            CreateMap<DeviceData, Device>();
            CreateMap<Device, DeviceData>()
                .AfterMap((original, changed)=>
                {
                    if (original.Classification != null)
                        changed.ClassificationId = original.Classification.Id;

                    if (original.Model != null)
                        changed.ModelId = original.Model.Id;

                    if (original.InitialLocation != null)
                        changed.InitialLocationId = original.InitialLocation.Id;

                    if (original.Document !=null && original.Document.Id != 0)
                    {
                        changed.Document = null;
                    }

                    if (original.Status != null && original.Status.Id != 0)
                    {
                        changed.Status = null;
                    }

                    if (original.OwnedBy != null && original.OwnedBy.Id != 0)
                    {
                        changed.OwnedBy = null;
                    }

                    if (original.InitialLocation != null && original.InitialLocation.Id != 0)
                    {
                        changed.InitialLocation = null;
                    }

                    if (original.Classification != null && original.Classification.Id != 0)
                    {
                        changed.Classification = null;
                    }

                    if (original.Model != null && original.Model.Id != 0)
                    {
                        changed.Model = null;
                    }
                });
            CreateMap<DeviceEventData, DeviceEvent>();
            CreateMap<DeviceEvent, DeviceEventData>();
            CreateMap<DetailedStatusData, DetailedStatus>();
            CreateMap<DetailedStatus, DetailedStatusData>();
            CreateMap<DeviceModelData, DeviceModel>();
            CreateMap<DeviceModel, DeviceModelData>();
            CreateMap<ClassificationData, Classification>();
            CreateMap<Classification, ClassificationData>();
            CreateMap<DeviceLocationData, DeviceLocation>();
            CreateMap<DeviceLocation, DeviceLocationData>();
            CreateMap<FileDefinition, FileDefinitionData>();
            CreateMap<FileDefinitionData, FileDefinition>();
        }
    }
}
