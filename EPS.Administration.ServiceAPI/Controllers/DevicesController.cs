using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.APICommunication.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
