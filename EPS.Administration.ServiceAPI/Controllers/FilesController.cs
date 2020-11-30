using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPS.Administration.Controllers.FileController;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.Device;
using EPS.Administration.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        private readonly IDeviceService _deviceService;
        private readonly string _documentStoragePath;

        public FilesController( IDetailedStatusService statusService, 
                                IClassificationService classification, 
                                IDeviceModelService modelService,
                                IDeviceLocationService locationSerivce,
                                IDeviceService deviceService,
                                IConfiguration configuration)
        {
            _statusService = statusService;
            _groupingService = classification;
            _deviceModelService = modelService;
            _deviceLocationService = locationSerivce;
            _deviceService = deviceService;

            _documentStoragePath = configuration["DocumentStorage"];
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
                    DeviceFileController filesController = new DeviceFileController(file.OpenReadStream(), _statusService, _groupingService, _deviceModelService, _deviceLocationService, _deviceService);

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

        [HttpPost("Document")]
        public async Task<ActionResult<FileUploadResponse>> UploadDocument([FromForm] IFormFile file)
        {
            string result = Path.GetRandomFileName().Split('.').First();
            string storedFileName = $"{result}.{file.FileName.Split('.').Last()}";
            string filePath = Path.Combine(_documentStoragePath, storedFileName);

            FileInfo directory = new FileInfo(filePath);
            directory.Directory.Create();

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new FileUploadResponse
            {
                UploadedFileInfo = new FileDefinition()
                {
                    FileName = file.FileName,
                    StoredFileName = storedFileName,
                }
            };
        }
    }
}
