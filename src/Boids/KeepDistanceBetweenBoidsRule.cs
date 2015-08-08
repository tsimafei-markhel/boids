using System;
using System.Numerics;

namespace Boids
{
    public class KeepDistanceBetweenBoidsRule : IFlockMoveRule
    {
        private float distanceTreshold;

        public KeepDistanceBetweenBoidsRule(float distanceTreshold)
        {
            if (distanceTreshold < 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(distanceTreshold));
            }

            this.distanceTreshold = distanceTreshold;
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

            Vector2 shift = Vector2.Zero;
            foreach (Boid boid in flock.Boids)
            {
                if (targetBoid != boid)
                {
                    if (Vector2.Distance(boid.Position, targetBoid.Position) < this.distanceTreshold)
                    {
                        shift -= boid.Position - targetBoid.Position;
                    }
                }
            }

            return shift;
        }
    }
}