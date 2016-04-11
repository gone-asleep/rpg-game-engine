using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public static class Extensions {
        private static readonly double PI2 = Math.PI * 2.0;

        public static float NextGaussian(this Random r, double sigma, double mu) {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            double randomNormal = Math.Sqrt(2.0 * -Math.Log(u1)) * Math.Sin(PI2 * u2);

            return (float)(mu + sigma * randomNormal);
        }

        public static float NextGaussian(double x, double sigma, double mu) {
            
            return (float)Math.Max(0,mu + sigma * Math.Sqrt(2.0 * -Math.Log(x)) * Math.Sin(PI2 * x));
        }

        public static double NormalProbabilityDensityFunction(double x, double mean, double standardDeviation) {
            const double sqrtTwoPiInv = 0.398942280401433;
            double z = (x - mean) / standardDeviation;
            return sqrtTwoPiInv * Math.Exp(-0.5 * z * z) / standardDeviation;
        }

        /// <summary>
        /// Returns the PDF of the standard normal distribution.
        /// </summary>
        /// <param name="x">Value at which the distribution is evaluated.</param>
        public static double StandardNormalProbabilityDensityFunction(double x) {
            const double SqrtTwoPiInv = 0.398942280401433;
            return SqrtTwoPiInv * Math.Exp(-0.5 * x * x);
        }

        
        public static float LogNormal(double x, double mu, double sigma) {
            double x1 = 1.0 / (sigma * x * Math.Sqrt(PI2));
            double x2 = Math.Pow((Math.Log(x) - mu), 2) / (2.0 * sigma * sigma);
            return (float)(x1 * Math.Exp(-x2));
        }

        public static float SkewNormal(double x, double mu, double sigma, double deviation) {
            double x1 = 1.0 / (sigma) * Math.Sqrt(PI2);
            double x2 = Math.Pow(Math.E, -Math.Pow(x - mu, 2) / (2 * sigma * sigma));
            return (float)(x1 * x2);
        }
    }


    public class Gaussian {
        private static bool uselast = true;
        private static double next_gaussian = 0.0;
        private static Random random = new Random();

        public static double BoxMuller() {
            if (uselast) {
                uselast = false;
                return next_gaussian;
            } else {
                double v1, v2, s;
                do {
                    v1 = 2.0 * random.NextDouble() - 1.0;
                    v2 = 2.0 * random.NextDouble() - 1.0;
                    s = v1 * v1 + v2 * v2;
                } while (s >= 1.0 || s == 0);

                s = System.Math.Sqrt((-2.0 * System.Math.Log(s)) / s);

                next_gaussian = v2 * s;
                uselast = true;
                return v1 * s;
            }
        }

        public static double BoxMuller(double mean, double standard_deviation) {
            return mean + BoxMuller() * standard_deviation;
        }

        // Will approximitely give a random gaussian integer between min and max so that min and max are at
        // 3.5 deviations from the mean (half-way of min and max).
        public static int Next(int min, int max) {
            double deviations = 3.5;
            int r;
            while ((r = (int)BoxMuller(min + (max - min) / 2.0, (max - min) / 2.0 / deviations)) > max || r < min) {
            }

            return r;
        }
    }
}
