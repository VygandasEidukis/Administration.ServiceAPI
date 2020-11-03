using System.Text;
using System.Threading.Tasks;
using EPS.Administration.Controllers.FileController;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPS.Administration.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IDetailedStatusService _statusService;
        private readonly IClassificationService _groupingService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IDeviceLocationService _deviceLocationService;
        public FilesController( IDetailedStatusService statusService, 
                                IClassificationService classification, 
                                IDeviceModelService modelService,
                                IDeviceLocationService locationSerivce)
        {
            _statusService = statusService;
            _groupingService = classification;
            _deviceModelService = modelService;
            _deviceLocationService = locationSerivce;
        }

        [AllowAnonymous]
        [HttpPost("uploadExtenderData")]
        public async Task<BaseResponse> UploadExtenderData([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || string.IsNullOrEmpty(file.FileName))
                {
                    throw new AdministrationException("File name or file does not exist.");
                }

                if (file.Length > 0)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //Needed because of: "No data is available for encoding 1252"
                    DeviceFileController filesController = new DeviceFileController(file.OpenReadStream(), _statusService, _groupingService, _deviceModelService, _deviceLocationService);

                    await filesController.ProcessFile();
                }
                return new BaseResponse
                {
                    Error = ErrorCode.OK,
                };
            }
            catch (AdministrationException ex)
            {
                //TODO: MEDIUM Add logging.
                return new BaseResponse
                {
                    Error = ErrorCode.InternalError,
                    Message = ex.Message
                };
            }
            catch
            {
                //TODO: MEDIUM Add logging.
                return new BaseResponse
                {
                    Error = ErrorCode.InternalError
                };
            }
        }
    }
}
