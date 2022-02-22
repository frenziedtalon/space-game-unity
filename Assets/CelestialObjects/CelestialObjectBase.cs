using Assets.CelestialObjects.Interfaces;
using Assets.Entities;
using Assets.Measurements;

namespace Assets.CelestialObjects
{

    public abstract class CelestialObjectBase: EntityBase, ICelestialObject
    {
        private Mass _mass;

        public Mass Mass => _mass;

        public void SetMass(Mass mass)
        {
            _mass = mass;
        }
    }
}
