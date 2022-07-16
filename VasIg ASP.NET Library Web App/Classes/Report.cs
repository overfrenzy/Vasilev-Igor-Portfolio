using System;
using System.ComponentModel.DataAnnotations;

namespace VasIgASP.NETLibraryWebApp.classes
{
    public class Report
    {
        [DataType(DataType.Date)]
        public DateTime? AcquirementDate { get; set; }

        public int BookCount { get; set; }
    }
}