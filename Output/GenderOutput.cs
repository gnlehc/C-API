namespace BootcampAPI.Output
{
    public class GenderOutput
    {
        public List<Gender> payload { get; set;}
        public GenderOutput() 
        { 
            payload = new List<Gender>();
        }
    }

    public class Gender
    {
        public int id { get; set; }
        public string description { get; set; }
    }
}
