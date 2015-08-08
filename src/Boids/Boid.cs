using System.Numerics;

namespace Boids
{
    public class Boid
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Position { get; set; } = Vector2.Zero;
    }
}