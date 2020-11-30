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
            CreateMap<Device, DeviceData>();

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
