namespace EPS.Administration.Models.Device
{
    public class Classification : IRevisionable
    {
        /// <summary>
        /// Identification number
        /// </summary>
        public int Id { get; set; }

        public int Revision { get; set; }

        /// <summary>
        /// Internal code of classification, defined by business logic
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Model of classification
        /// </summary>
        public string Model { get; set; }

        public override string ToString()
        {
            return Model;
        }
    }
}