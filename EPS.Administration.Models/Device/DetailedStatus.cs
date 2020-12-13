namespace EPS.Administration.Models.Device
{
    public class DetailedStatus : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }

        /// <summary>
        /// Device status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Device status description
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Status) ? "New status" : Status;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DetailedStatus))
                return false;

            return ((DetailedStatus)obj).Id == this.Id;
        }
    }
}