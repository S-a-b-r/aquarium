using System.Collections.Generic;

namespace aquarium
{
    class Food : Movier
    {
        public override void Draw()
        {
            ConsoleEx.Print((int)x, (int)y, ".");
        }
        public override (double dx, double dy) GetNextMove(IEnumerable<Movier> others)
        {
            return (0, ground ? 0 : speed);
        }
        public override void Move(double dx, double dy, Collision c)
        {
            if (c == Collision.Horizontal)
                ground = true;
            x += dx;
            y += dy;
        }
    }
}