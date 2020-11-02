namespace EPS.Administration.DAL.Data
{
    public class DetailedStatusData
    {
        public int Id { get; set; }
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
