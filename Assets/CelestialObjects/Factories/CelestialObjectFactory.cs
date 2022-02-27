using Assets.CelestialObjects.Classes;
using Assets.CelestialObjects.OrbitalMechanics;
using System;
using UnityEngine;

namespace Assets.CelestialObjects.Factories
{
    public static class CelestialObjectFactory
    {
        const string tagCelestial = "Celestial";

        public static GameObject CreateCelestialObject(
            PhysicalData physicalData,
            OrbitData orbitData = null
        )
        {
            switch (physicalData.Type)
            {
                case CelestialObjectType.Star:
                    return CreateStar(physicalData);
                case CelestialObjectType.Planet:
                    return CreatePlanet(physicalData, orbitData);
                default:
                    throw new NotImplementedException();
            }
        }

        public static GameObject CreateStar(
            PhysicalData physicalData
        )
        {
            var gameObject = new GameObject(physicalData.Name, typeof(SphereCollider))
            {
                tag = tagCelestial
            };
            gameObject.SetActive(false);

            var star = gameObject.AddComponent<Star>();
            star.SetMass(physicalData.Mass);
            star.SetRadius(physicalData.Radius);

            // TODO: This is a hack. Fix it when implementing orbits
            gameObject.SetActive(true);

            return gameObject;
        }

        public static GameObject CreatePlanet(
            PhysicalData physicalData,
            OrbitData orbitData
        )
        {
            var gameObject = new GameObject(physicalData.Name, typeof(SphereCollider))
            {
                tag = tagCelestial
            };
            gameObject.SetActive(false);

            var planet = gameObject.AddComponent<Planet>();
            planet.SetMass(physicalData.Mass);
            planet.SetRadius(physicalData.Radius);

            var orbit = new Orbit(orbitData);

            // TODO: Pass the primary through
            planet.SetOrbit(null, orbit);

            // TODO: This is a hack. Fix it when implementing orbits
            gameObject.SetActive(true);

            return gameObject;
        }
    }
}
