namespace WebApiStone.Entities.Values
{
    public class EducationValues
    {
        public static readonly string ILLITERATE = "Analfabeto(a)";

        public static readonly string LITERATE = "Alfabetizado(a)";

        public static readonly string ELEMENTARY = "Ensino Fundamental";

        public static readonly string HIGH_SCHOOL = "Ensino Médio";

        public static readonly string COLLEGE = "Ensino Superior";

        public static readonly string GRADSCHOOL = "Pós-Graduação";

        public static readonly string NA = "Não Informada";

        public static List<string> Values()
        {
            return new List<string>()
            {
               ILLITERATE, LITERATE, ELEMENTARY, HIGH_SCHOOL, COLLEGE, GRADSCHOOL, NA
            };
        }
    }
}
