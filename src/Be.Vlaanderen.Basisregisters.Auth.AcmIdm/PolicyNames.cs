namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    public static class PolicyNames
    {
        public static class Adres
        {
            public const string DecentraleBijwerker = "adres-decentrale-bijwerker";
            public const string InterneBijwerker = "adres-interne-bijwerker";
        }

        public static class GeschetstGebouw
        {
            public const string DecentraleBijwerker = "geschetst-gebouw-decentrale-bijwerker";
            public const string Omgeving = "geschetst-gebouw-omgeving";
            public const string InterneBijwerker = "geschetst-gebouw-interne-bijwerker";
        }

        public static class IngemetenGebouw
        {
            public const string GrbBijwerker = "ingemeten-gebouw-grb-bijwerker";
            public const string InterneBijwerker = "ingemeten-gebouw-interne-bijwerker";
        }

        public static class GeschetsteWeg
        {
            public const string Beheerder = "wegen-geschetste-weg-beheerder";
        }

        public static class IngemetenWeg
        {
            public const string Beheerder = "wegen-ingemeten-weg-beheerder";
        }

        public static class WegenAttribuutWaarden
        {
            public const string Beheerder = "wegen-attribuutwaarden-beheerder";
        }

        public static class WegenUitzonderingen
        {
            public const string Beheerder = "wegen-uitzonderingen-beheerder";
        }
    }
}
