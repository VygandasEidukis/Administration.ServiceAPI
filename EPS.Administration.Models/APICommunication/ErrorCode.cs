namespace EPS.Administration.Models.APICommunication
{
    public enum ErrorCode : int
    {
        OK = 0,
        InternalError = 1,
        BadUsernameOrPassword = 2,
        ServiceError = 3,
        ValidationError = 4,
    }
}