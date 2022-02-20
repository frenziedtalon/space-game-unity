namespace Assets.Measurements
{
    public class Distance
    {
        private readonly double _kilometres;
        private const double KilometresInAstronomicalUnit = 149597870.691;

        public double Kilometres => _kilometres;
        public double AstronomicalUnits => _kilometres / KilometresInAstronomicalUnit;

        private Distance(double kilometres)
        {
            _kilometres = kilometres;
        }

        public static Distance FromKilometres(double kilometres)
        {
            return new Distance(kilometres);
        }

        public static Distance FromAstronomicalUnits(double astronomicalUnits)
        {
            return new Distance(astronomicalUnits * KilometresInAstronomicalUnit);
        }
    }
}
