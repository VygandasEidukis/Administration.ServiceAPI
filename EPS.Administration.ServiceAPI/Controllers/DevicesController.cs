using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.APICommunication.Filter;
using EPS.Administration.Models.Device;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IClassificationService _classificationService;
        private readonly IDeviceLocationService _locationService;
        private readonly IDetailedStatusService _statusService;
        private readonly IDeviceModelService _modelService;

        public DevicesController(IDeviceService deviceService, IClassificationService classificationService, IDeviceLocationService locationService, IDetailedStatusService statusService, IDeviceModelService modelService)
        {
            _deviceService = deviceService;
            _classificationService = classificationService;
            _locationService = locationService;
            _statusService = statusService;
            _modelService = modelService;
        }

        [HttpPost("GetFiltered")]
        public ActionResult<GetDevicesResponse> GetDevices(DeviceFilter filter)
        {
            var devices = _deviceService.Get(filter);
            var overall = _deviceService.BaseDeviceCount();
            int pages = overall / filter.PageSize;
            if ((overall % filter.PageSize) != 0)
            {
                pages++;
            }

            return new GetDevicesResponse()
            {
                Count = filter.PageSize,
                Devices = devices,
                From = filter.PageSize * filter.Page,
                Overall = overall,
                Pages = pages
            };
        }

        [HttpGet]
        public ActionResult<DeviceResponse> Get(string serialNumber)
        {
            return new DeviceResponse()
            {
                RecievedDevice = _deviceService.Get(serialNumber)
            };
        }

        [HttpGet("GetMetadata")]
        public ActionResult<DeviceMetadataResponse> GetMetadata()
        {
            var metadata = _deviceService.GetMetadata();

            return metadata;
        }

        [HttpPost("AddOrUpdate")]
        public ActionResult<BaseResponse> AddOrUpdateDevice(Device device)
        {
            try
            {
                _deviceService.AddOrUpdate(device);

                return new BaseResponse()
                {
                    Message = "Successfully added or updated the device"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Error = ErrorCode.ServiceError,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("Models")]
        public ActionResult<GetModelResponse> GetModels()
        {
            return new GetModelResponse()
            {
                Models = _modelService.Get()
            };
        }

        [HttpGet("Statuses")]
        public ActionResult<GetStatusResponse> GetStatuses()
        {
            return _deviceService.GetStatus();
        }

        [HttpGet("Classifications")]
        public ActionResult<GetClassificationResponse> GetClassifications()
        {
            return _deviceService.GetClassification();
        }

        [HttpGet("Locations")]
        public ActionResult<GetLocationResponse> GetLocations()
        {
            return _deviceService.GetLocation();
        }

        [HttpPost("Statuses")]
        public ActionResult<BaseResponse> UpdateStatus(DetailedStatus status)
        {
            try
            {
                _statusService.AddOrUpdate(status);

                if (status.Id != 0)
                {
                    var oldStatus = _statusService.Get(status.Id);
                    var newStatus = _statusService.Get(status.Status);
                    _deviceService.UpdateStatuses(newStatus, oldStatus);
                }

                return new BaseResponse()
                {
                    Message = $"Updated/Added status: {status.Status}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Error = ErrorCode.InternalError,
                    Message = ex.Message
                };
            }
        }

        [HttpPost("Classifications")]
        public ActionResult<BaseResponse> UpdateClassification(Classification classification)
        {
            try
            {
                _classificationService.AddOrUpdate(classification);

                if (classification.Id != 0)
                {
                    var oldClassification = _classificationService.Get(classification.Id);
                    var newClassification = _classificationService.Get(classification.Code);
                    _deviceService.UpdateClassifications(newClassification, oldClassification);
                }

                return new BaseResponse()
                {
                    Message = $"Updated/Added classification: {classification.Model}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Error = ErrorCode.InternalError,
                    Message = ex.Message
                };
            }
        }

        [HttpPost("Locations")]
        public ActionResult<BaseResponse> UpdateLocation(DeviceLocation location)
        {
            try
            {
                _locationService.AddOrUpdate(location);

                if (location.Id != 0)
                {
                    var oldLocation = _locationService.Get(location.Id);
                    var newLocation = _locationService.Get(location.Name);
                    _deviceService.UpdateLocations(newLocation, oldLocation);
                }

                return new BaseResponse()
                {
                    Message = $"Updated/Added location: {location.Name}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Error = ErrorCode.InternalError,
                    Message = ex.Message
                };
            }
        }

        [HttpPost("Models")]
        public ActionResult<BaseResponse> UpdateModels(DeviceModel model)
        {
            try
            {
                _modelService.AddOrUpdate(model);

                if (model.Id != 0)
                {
                    var oldModel = _modelService.Get(model.Id);
                    var newModel = _modelService.Get(model.Name);
                    _deviceService.UpdateModels(newModel, oldModel);
                }

                return new BaseResponse()
                {
                    Message = $"Updated/Added model: {model.Name}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Error = ErrorCode.InternalError,
                    Message = ex.Message
                };
            }
        }
    }
}
