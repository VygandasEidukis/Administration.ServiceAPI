using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceEventService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
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
        }
    }

    public class NoMapAttribute : System.Attribute
    {
    }
    public static class IgnoreNoMapExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute != null)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            }
            return expression;
        }
    }
}
