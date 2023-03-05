using System.ComponentModel.DataAnnotations;

namespace BootcampAPI.Models
{
    public class MsReligion
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
    }
}
