using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public class Classification
    {
        /// <summary>
        /// Identification number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Internal code of classification, defined by business logic
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Model of classification
        /// </summary>
        public string Model { get; set; }
    }
}
