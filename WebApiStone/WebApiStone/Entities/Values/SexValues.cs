namespace WebApiStone.Entities.Values
{
    public class SexValues
    {
        public static readonly string FEMALE = "F";

        public static readonly string MALE = "M";

        public static readonly string NA = "Não Informada";
            
        public static List<string> Values()
        {
            return new List<string>()
            {
                FEMALE, MALE, NA
            };
        }
    }

}
