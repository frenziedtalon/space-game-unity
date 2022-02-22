namespace Assets.Measurements
{
    public class Distance
    {
        private readonly double _kilometres;
        private const double KilometresInAstronomicalUnit = 149597870.691;
        private const double KilometresInScaledUnit = 10e3;


        public double Kilometres => _kilometres;
        public double AstronomicalUnits => _kilometres / KilometresInAstronomicalUnit;
        public double ScaledUnits => _kilometres / KilometresInScaledUnit;

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
