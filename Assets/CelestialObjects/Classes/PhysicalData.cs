using Assets.Measurements;

namespace Assets.CelestialObjects.Classes
{
    // TODO: Is this actually needed?
    public class PhysicalData
    {
        public string Name { get; }
        public Distance Radius { get; }
        public Mass Mass { get; }
        public CelestialObjectType Type { get; }

        public PhysicalData(
            string name,
            Mass mass,
            Distance radius,
            CelestialObjectType type
        )
        {
            Name = name;
            Radius = radius;
            Mass = mass;
            Type = type;
        }
    }
}
