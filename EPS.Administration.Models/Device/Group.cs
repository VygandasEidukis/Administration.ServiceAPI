using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public class Group
    {
        /// <summary>
        /// Auto generated identification number for the group
        /// </summary>
        public int Id { get; set; }

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
