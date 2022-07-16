using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VasIgASP.NETLibraryWebApp.Classes
{
    public class Location
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Adress { get; set; }
        [DisplayName("Location Type")]
        public LocationType LocationType { get; set; }

        public virtual List<Book> Books { get; set; }
    }
    public enum LocationType
    {
        Storage,
        Library,
        Recovery
    }
}
