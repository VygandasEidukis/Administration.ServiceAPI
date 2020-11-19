using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.Models.APICommunication;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<GetDevicesResponse> Get(int from, int count, string query, string orderQuery, bool reversed)
        {
            var devices = _deviceService.Get(from, count, query, orderQuery, reversed);
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
