using System;
using System.Drawing;
using System.Numerics;

namespace Boids
{
    public class AvoidBordersRule : IFlockMoveRule
    {
        public Rectangle AllowedArea { get; set; }
        private float proximityTreshold;

        public AvoidBordersRule(Rectangle allowedArea, float proximityTreshold)
        {
            if (proximityTreshold <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(proximityTreshold));
            }

            this.AllowedArea = allowedArea;
            this.proximityTreshold = proximityTreshold;
        }

        public Vector2 GetVelocity(Boid targetBoid, Flock flock)
        {
            if (targetBoid == null)
            {
                throw new ArgumentNullException(nameof(targetBoid));
            }

            Vector2 shift = Vector2.Zero;

            if (Math.Abs(this.AllowedArea.Left - targetBoid.Position.X) < this.proximityTreshold)
            {
                shift += new Vector2(-targetBoid.Velocity.X, 0.0f);
            }

            if (Math.Abs(this.AllowedArea.Right - targetBoid.Position.X) < this.proximityTreshold)
            {
                shift += new Vector2(-targetBoid.Velocity.X, 0.0f);
            }

            if (Math.Abs(this.AllowedArea.Top - targetBoid.Position.Y) < this.proximityTreshold)
            {
                shift += new Vector2(0.0f, -targetBoid.Velocity.Y);
            }

            if (Math.Abs(this.AllowedArea.Bottom - targetBoid.Position.Y) < this.proximityTreshold)
            {
                shift += new Vector2(0.0f, -targetBoid.Velocity.Y);
            }

            return shift;
        }
    }
}