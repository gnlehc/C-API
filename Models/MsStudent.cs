using System.ComponentModel.DataAnnotations;

namespace BootcampAPI.Models
{
    public class MsStudent
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthdate { get; set; }
        public int genderId { get; set; }
        public int religionId { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
}
