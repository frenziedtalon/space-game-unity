namespace Assets.Measurements
{
    public class Mass
    {
        private readonly double _kilograms;
        private const double KilogramsInSolarMass = 1.98855 * 10e30;
        private const double KilogramsInEarthMass = 5973.6 * 10e21;
        private const double KilogramsInScaledUnit = 10e24;

        public double Kilograms => _kilograms;
        public double SolarMasses => _kilograms / KilogramsInSolarMass;
        public double EarthMasses => _kilograms / KilogramsInEarthMass;
        public double ScaledUnits => EarthMasses;

        private Mass(double kilograms)
        {
            _kilograms = kilograms;
        }

        public static Mass FromKilograms(double kilograms)
        {
            return new Mass(kilograms);
        }

        public static Mass FromSolarMasses(double solarMasses)
        {
            return new Mass(solarMasses * KilogramsInSolarMass);
        }

        public static Mass FromEarthMasses(double solarMasses)
        {
            return new Mass(solarMasses * KilogramsInEarthMass);
        }
    }
}