using Assets.Measurements;
using UnityEngine;

namespace Assets.CelestialObjects
{
    public static class CelestialObjectFactory
    {
        const string tagCelestial = "Celestial";

        public static GameObject CreateStar(
            string name,
            float mass, // 10^24 kg
            float radius // 10^6 km
        )
        {
            var gameObject = new GameObject(name, typeof(SphereCollider))
            {
                tag = tagCelestial
            };

            var star = gameObject.AddComponent<Star>();
            star.SetMass(Mass.FromKilograms(mass * 10e24));
            star.SetRadius(Distance.FromKilometres(radius * 10e6));
                        
            return gameObject;
        }


    }
}
