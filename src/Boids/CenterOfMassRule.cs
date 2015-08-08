using System;
using System.Numerics;

namespace Boids
{
    public class CenterOfMassRule : IFlockMoveRule
    {
        private float shiftCoefficient;

        public CenterOfMassRule() : this(1.0f)
        {
        }

        public CenterOfMassRule(float shiftCoefficient)
        {
            if (shiftCoefficient <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(shiftCoefficient));
            }

            this.shiftCoefficient = shiftCoefficient;
        }

        public Vector2 GetVelocity(Boid targetBoid, Flock flock)
        {
            if (targetBoid == null)
            {
                throw new ArgumentNullException(nameof(targetBoid));
            }

            if (flock == null)
            {
                throw new ArgumentNullException(nameof(flock));
            }

            if (!flock.Boids.Contains(targetBoid)) // TODO: Time-consuming?..
            {
                throw new InvalidOperationException();
            }

            if (flock.Boids.Count < 2)
            {
                return Vector2.Zero;
            }

            Vector2 centerOfMass = Vector2.Zero;
            foreach (Boid boid in flock.Boids)
            {
                if (targetBoid != boid)
                {
                    centerOfMass += boid.Position;
                }
            }

            centerOfMass /= flock.Boids.Count - 1;
            return (centerOfMass - targetBoid.Position) * this.shiftCoefficient;
        }
    }
}