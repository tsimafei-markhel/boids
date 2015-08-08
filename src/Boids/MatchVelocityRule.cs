using System;
using System.Numerics;

namespace Boids
{
    public class MatchVelocityRule : IFlockMoveRule
    {
        private float correctionCoefficient;

        public MatchVelocityRule(float correctionCoefficient)
        {
            if (correctionCoefficient <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(correctionCoefficient));
            }

            this.correctionCoefficient = correctionCoefficient;
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

            Vector2 velocity = new Vector2();
            foreach (Boid boid in flock.Boids)
            {
                if (targetBoid != boid)
                {
                    velocity += boid.Velocity;
                }
            }

            velocity /= flock.Boids.Count - 1;
            return (velocity - targetBoid.Velocity) * this.correctionCoefficient;
        }
    }
}