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
    }
}
