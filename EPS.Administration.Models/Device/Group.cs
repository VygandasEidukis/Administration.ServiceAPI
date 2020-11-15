namespace EPS.Administration.Models.Device
{
    public class Group : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }

        /// <summary>
        /// Short code for the group identification
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Title for the group
        /// </summary>
        public string Title { get; set; }
    }
}