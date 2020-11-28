namespace EPS.Administration.Models.Device
{
    public class DeviceLocation
    {
        public int Id { get; set; }
        public int BaseId { get; set; }
        public int Revision { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return Name ?? "New location";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DeviceLocation))
                return false;

            return ((DeviceLocation)obj).Id == this.Id;
        }
    }
}