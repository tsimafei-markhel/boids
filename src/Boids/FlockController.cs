using System;
using System.Collections.Generic;
using System.Numerics;

namespace Boids
{
    public class FlockController
    {
        public ICollection<IFlockMoveRule> MoveRules { get; } = new List<IFlockMoveRule>();

        public virtual void Move(Flock flock)
        {
            if (flock == null)
            {
                throw new ArgumentNullException(nameof(flock));
            }

            foreach (Boid boid in flock.Boids)
            {
                this.MoveBoid(flock, boid);
            }
        }

        protected virtual void MoveBoid(Flock flock, Boid boid)
        {
            if (flock == null)
            {
                throw new ArgumentNullException(nameof(flock));
            }

            if (boid == null)
            {
                throw new ArgumentNullException(nameof(boid));
            }

            Vector2 aggregateVelocity = boid.Velocity;
            foreach (IFlockMoveRule rule in this.MoveRules)
            {
                aggregateVelocity += rule.GetVelocity(boid, flock);
            }

            boid.Velocity = aggregateVelocity;
            boid.Position += aggregateVelocity;
        }
    }
}