namespace Assets.Measurements
{
    public class Distance
    {
        // Astronomical range                   Typical units
        // Distances to satellites              kilometres
        // Distances to near-Earth objects      lunar distance
        // Planetary distances                  astronomical units, gigametres
        // Distances to nearby stars            parsecs, light-years
        // Distances at the galactic scale      kiloparsecs
        // Distances to nearby galaxies         megaparsecs

        private readonly double _kilometres;
        private const double KilometresInAstronomicalUnit = 149597870.691;

        private const double KilometresInScaledUnitForOrbit = 1e5;
        private const double KilometresInScaledUnitForRadius = 1e4;


        public double Kilometres => _kilometres;
        public double AstronomicalUnits => _kilometres / KilometresInAstronomicalUnit;

        // TODO: Not ideal having these properties within this class
        public double ScaledUnitsOrbit => _kilometres / KilometresInScaledUnitForOrbit;
        public double ScaledUnitsRadius => _kilometres / KilometresInScaledUnitForRadius;

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
