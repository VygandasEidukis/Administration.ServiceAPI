namespace EPS.Administration.Models.Device
{
    public class DeviceModel : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DeviceModel))
                return false;

            return ((DeviceModel)obj).Id == this.Id;
        }
    }
}