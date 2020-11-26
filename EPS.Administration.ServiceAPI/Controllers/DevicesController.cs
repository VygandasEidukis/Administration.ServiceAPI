using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.APICommunication.Filter;
using EPS.Administration.Models.Device;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EPS.Administration.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
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
    }
}
