namespace BootcampAPI.Input
{
    public class StudentInput
    {
        public int? studentId {  get; set; }
        public string? name { get; set; }
        public DateTime BirthDate { get; set; }
        public int? gender { get; set; }
        public int? religion { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public List<ScoreInput>? ScoreList { get; set; }
    }
    public class ScoreInput
    {
        public int? subjectId { get; set; }
        public int? semester1Score { get; set; }
        public int? semester2Score { get; set; }
    }
}
