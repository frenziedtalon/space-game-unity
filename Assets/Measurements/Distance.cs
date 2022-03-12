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
        private const double KilometresInGigaMetre = 1e6;
        private const double KilometresInAstronomicalUnit = 149597870.691;

        private const double KilometresInScaledUnitForOrbit = KilometresInScaledUnitForRadius * 125;
        private const double KilometresInScaledUnitForRadius = EarthRadiusInKilometres / ScaledEarthRadius;

        private const double EarthRadiusInKilometres = 6371;
        private const double ScaledEarthRadius = 100;

        public double Kilometres => _kilometres;
        public double GigaMetres => _kilometres / KilometresInGigaMetre;
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

        public static Distance FromGigaMetres(double gigametres)
        {
            return new Distance(gigametres * KilometresInGigaMetre);
        }

        public static Distance FromAstronomicalUnits(double astronomicalUnits)
        {
            return new Distance(astronomicalUnits * KilometresInAstronomicalUnit);
        }
    }
}
