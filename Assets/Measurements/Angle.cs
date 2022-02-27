using System;

namespace Assets.Measurements
{
    /// <summary>
    ///     ''' Represents the concept of an angle and allows easily switching between the different unit types.
    ///     ''' </summary>
    ///     ''' <remarks>
    ///     ''' Any angle less than zero or greater than (or equal to) 360 (or 2*Pi) will be corrected until it is within these limits
    ///     ''' Using degrees as the internal value. Found that this results in fewer rounding errors than operating with Pi repeatedly.
    ///     ''' </remarks>
    public class Angle
    {
        private readonly double _degrees;
        private readonly int _decimalPlaces;

        const int DefaultDecimalPlaces = 14;
        const MidpointRounding RoundingMethod = MidpointRounding.AwayFromZero;

        public double Radians => Math.Round(ConvertDegreesToRadians(_degrees), _decimalPlaces, RoundingMethod);
        public double Degrees => Math.Round(_degrees, _decimalPlaces, RoundingMethod);

        private Angle(double degrees, int decimalPlaces = DefaultDecimalPlaces)
        {
            _degrees = CalculateStandardPosition(degrees);

            _decimalPlaces = decimalPlaces > DefaultDecimalPlaces ? DefaultDecimalPlaces : decimalPlaces;
        }

        public static Angle FromRadians(double radians)
        {
            return new Angle(ConvertRadiansToDegrees(radians));
        }

        public static Angle FromDegrees(double degrees)
        {
            return new Angle(degrees);
        }

        /// <summary>
        /// Converts coterminal angles to standard position, i.e. to between 0 - 360 degrees
        /// </summary>
        private double CalculateStandardPosition(double degrees)
        {
            return (degrees % 360) + (degrees < 0 ? 360 : 0);
        }

        private static double ConvertRadiansToDegrees(double radians)
        {
            return radians / (Math.PI / 180);
        }

        private static double ConvertDegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        /// <summary>
        /// The number of decimal places to which all results will be rounded
        /// </summary>
        public int DecimalPlaces => _decimalPlaces;
    }
}
