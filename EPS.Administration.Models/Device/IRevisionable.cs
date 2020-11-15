namespace EPS.Administration.Models.Device
{
    public interface IRevisionable
    {
        int Id { get; set; }
        int Revision { get; set; }
    }
}