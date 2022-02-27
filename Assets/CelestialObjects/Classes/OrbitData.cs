using Assets.Measurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CelestialObjects.Classes
{
    public class OrbitData
    {
        public Distance SemiMajorAxis { get; }
        public double Eccentricity { get; }
        public Angle Inclination { get; }
        public Angle ArgumentOfPeriapsis { get; }
        public Angle LongitudeOfAscendingNode { get; }
        public Angle MeanAnomalyZero { get; }

        public OrbitData(
            Distance semiMajorAxis,
            double eccentricity,
            Angle inclination,
            Angle argumentOfPeriapsis,
            Angle longitudeOfAscendingNode,
            Angle meanAnomalyZero
        )
        {
            SemiMajorAxis = semiMajorAxis;
            Eccentricity = eccentricity;
            Inclination = inclination;
            ArgumentOfPeriapsis = argumentOfPeriapsis;
            LongitudeOfAscendingNode = longitudeOfAscendingNode;
            MeanAnomalyZero = meanAnomalyZero;
        }
    }
}
