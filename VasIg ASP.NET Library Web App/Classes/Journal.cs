using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VasIgASP.NETLibraryWebApp.Classes
{
    public class Journal
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public DateTime Date { get; set; }
        public int FromId { get; set; }
        [ForeignKey("FromId")]
        public virtual Location From { get; set; }
        public int ToId { get; set; }
        [ForeignKey("ToId")]
        public virtual Location To { get; set; }
    }
}
