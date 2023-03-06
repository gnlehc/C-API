namespace BootcampAPI.Output
{
    public class StudentOutput
    {
        public studentData? payload { get; set; }
        public StudentOutput()
            {
                payload = new studentData();
            }
    }
    public class Score
    {
        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public decimal semester1Score { get; set; }
        public decimal semester2Score { get; set; }
        public decimal finalScore { get; set; }
    }

    public class studentData
    {
        public Student student { get; set; }
        public DateTime birthdate { get; set; }
        public Gender gender { get; set; }
        public religion religion { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public List<Score> scoreList { get; set; }
        public decimal averageFinalScore { get; set; }
        public int grade { get; set; }

    }

    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
