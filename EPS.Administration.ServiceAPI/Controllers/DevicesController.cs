using System.Collections.Generic;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.Device;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPS.Administration.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public ActionResult<GetDevicesResponse> Get(int from, int count)
        {
            var devices = _deviceService.Get(from, count);
            var overall = _deviceService.BaseDeviceCount();
            int pages = overall / count;
            if ((overall % count) != 0)
            {
                pages++;
            }

            return new GetDevicesResponse()
            {
                Count = count,
                Devices = devices,
                From = from,
                Overall = overall,
                Pages = pages
            };

        }
    }
}
