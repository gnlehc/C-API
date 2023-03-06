using BootcampAPI.Output;

namespace BootcampAPI.Helper
{

    public class ReligionHelper
    {
        private SchoolDBContext dBContext;
        public ReligionHelper(SchoolDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public List<religion> ReligionList()
        {
            var returnValue = new List<religion>();
            try
            {
                var religion = dBContext.MsReligion.ToList();
                returnValue = religion.Select(x => new religion()
                {
                    id = x.id,
                    description = x.description,
                }).ToList();
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
