using System.ComponentModel.DataAnnotations;

namespace BootcampAPI.Models
{
    public class MsGrade
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public decimal minScore { get; set; }
        public decimal maxScore { get; set; }

    }
}
