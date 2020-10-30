using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.File;
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
        private string _exceptionText { get; set; }

        [AllowAnonymous]
        [HttpPost("uploadExtenderData")]
        public BaseResponse UploadExtenderData([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || string.IsNullOrEmpty(file.FileName))
                {
                    _exceptionText = "File name or file does not exist";
                    throw new Exception();
                }

                //TODO: HIGH handle upload and data export

                return new BaseResponse
                {
                    Error = ErrorCode.OK,
                };
            }
            catch
            {
                return new BaseResponse
                {
                    Error = ErrorCode.InternalError,
                    Message = string.IsNullOrEmpty(_exceptionText) ? "Failed to upload an image" : _exceptionText
                };
            }
        }
    }
}
