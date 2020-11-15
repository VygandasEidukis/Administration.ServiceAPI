namespace EPS.Administration.Models.Device
{
    public class DeviceModel : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}