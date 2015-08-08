using System.Numerics;

namespace Boids
{
    public interface IFlockMoveRule
    {
        Vector2 GetVelocity(Boid targetBoid, Flock flock);
    }
}