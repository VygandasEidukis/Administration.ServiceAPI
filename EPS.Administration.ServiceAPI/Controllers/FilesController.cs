using System.Text;
using System.Threading.Tasks;
using EPS.Administration.Controllers.FileController;
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
                    ExtenderFileController filesController = new ExtenderFileController(file.OpenReadStream());

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
