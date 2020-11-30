using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.APICommunication
{
    public class FileUploadResponse : BaseResponse
    {
        public FileDefinition UploadedFileInfo { get; set; }
    }
}
