namespace EPS.Administration.Models.APICommunication
{
    public class BaseResponse
    {
        public ErrorCode Error { get; set; }
        public string Message { get; set; }
    }
}