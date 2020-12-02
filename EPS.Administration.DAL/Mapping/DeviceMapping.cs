using AutoMapper;
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
                .AfterMap((a, b)=>
                {
                    if (a.Classification != null)
                        b.ClassificationId = a.Classification.Id;

                    if (a.Model != null)
                        b.ModelId = a.Model.Id;

                    if (a.InitialLocation != null)
                        b.InitialLocationId = a.InitialLocation.Id;
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
