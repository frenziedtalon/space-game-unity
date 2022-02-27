using Assets.CelestialObjects.Interfaces;
using Assets.CelestialObjects.OrbitalMechanics;
using Assets.Entities;
using Assets.Measurements;
using System;
using System.Collections.Generic;

namespace Assets.CelestialObjects.BaseObjects
{

    public abstract class CelestialObjectBase : EntityBase, ICelestialObject
    {
        private Mass _mass;

        public Mass Mass => _mass;
        public List<OrbitingCelestialObjectBase> Satellites = new List<OrbitingCelestialObjectBase>();

        public void SetMass(Mass mass)
        {
            _mass = mass;
        }

        public void AddSatellite(OrbitingCelestialObjectBase satellite, Orbit orbit)
        {
            if (satellite == null)
            {
                throw new ArgumentNullException(nameof(satellite));
            }

            if (orbit == null)
            {
                throw new ArgumentNullException(nameof(orbit));
            }

            satellite.SetOrbit(this, orbit);
            Satellites.Add(satellite);
        }
    }
}
