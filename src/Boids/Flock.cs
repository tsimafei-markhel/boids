using System.Collections.Generic;

namespace Boids
{
    public class Flock
    {
        public ICollection<Boid> Boids { get; private set; } = new List<Boid>();
    }
}