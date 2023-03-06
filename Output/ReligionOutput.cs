namespace BootcampAPI.Output
{
    public class ReligionOutput
    {
        public List<religion> payload { get; set; }
        public ReligionOutput()
        {
            payload = new List<religion>();
        }
    }
    public class religion
    {
        public int id { get; set; }
        public string description { get; set; }
    }
}
