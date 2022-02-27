using Assets.CelestialObjects.OrbitalMechanics;
using System;

namespace Assets.CelestialObjects.BaseObjects
{
    public abstract class OrbitingCelestialObjectBase : CelestialObjectBase
    {
        public Orbit Orbit { get; set; }
        public Guid Primary { get; set; }

        public void SetOrbit(CelestialObjectBase primary, Orbit orbit)
        {
            // TODO: Pass the primary through
            //if (primary == null)
            //{
            //    throw new ArgumentNullException(nameof(primary));
            //}

            Orbit = orbit ?? throw new ArgumentNullException(nameof(orbit));

            // TODO: If parent / satellite mass can ever be changed in the future this needs to change
            //Orbit.MassOfPrimary = primary.Mass;
            //Orbit.MassOfSatellite = Mass;

            //Primary = primary.Id;
        }
    }
}
