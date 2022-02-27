using Assets.CelestialObjects.Classes;
using Assets.Measurements;

namespace Assets.CelestialObjects.OrbitalMechanics
{
    public class Orbit
    {
        private OrbitData _data;
        public Distance SemiMajorAxis => _data.SemiMajorAxis;
        public double Eccentricity => _data.Eccentricity;
        public Angle Inclination => _data.Inclination;
        public Angle ArgumentOfPeriapsis => _data.ArgumentOfPeriapsis;
        public Angle LongitudeOfAscendingNode => _data.LongitudeOfAscendingNode;
        public Angle MeanAnomalyZero => _data.MeanAnomalyZero;

        public Mass MassOfPrimary { get; set; }
        public Mass MassOfSatellite { get; set; }

        public Orbit(OrbitData orbitData)
        {
            _data = orbitData;
        }
    }
}
