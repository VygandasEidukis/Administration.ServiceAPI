namespace EPS.Administration.Models.APICommunication
{
    public class LogInResponse : BaseResponse
    {
        public string Token { get; set; }
        public LogInResponse()
        {

        }

        public LogInResponse(string token)
        {
            this.Token = token;
        }
    }
}
