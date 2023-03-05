using System.ComponentModel.DataAnnotations;

namespace BootcampAPI.Models
{
    public class MsSubject
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
