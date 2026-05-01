namespace SchoolDMS.API.Helpers
{
    public static class GeoLocationHelper
    {
        private const double EarthRadiusKm = 6371.0;

        /// <summary>
        /// Calculates the distance between two GPS coordinates in kilometers.
        /// </summary>
        public static double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private static double ToRadians(double angleIn10thofaDegree)
        {
            return (angleIn10thofaDegree * Math.PI) / 180;
        }

        /// <summary>
        /// Checks if the provided coordinates are within the specified tolerance (in meters).
        /// </summary>
        public static bool IsWithinTolerance(double lat1, double lon1, double lat2, double lon2, double toleranceMeters = 500)
        {
            double distanceKm = CalculateDistanceKm(lat1, lon1, lat2, lon2);
            double distanceMeters = distanceKm * 1000;
            return distanceMeters <= toleranceMeters;
        }
    }
}
