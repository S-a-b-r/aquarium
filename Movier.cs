using System;
using System.Collections.Generic;
using System.Linq;

namespace aquarium
{
    class Movier
    {
        public delegate void FuncType (Movier obj);
        public FuncType OnSpawn;

        public double x, y, size, speed;
        public int hp, damage;
        public bool ground;

        public virtual (double dx, double dy) GetNextMove(IEnumerable<Movier> others) { return (0,0); }
        public virtual void Move(double dx, double dy, Collision c) { }
        public virtual void Touch(Movier other) { }
        public virtual void Draw() { }
        public virtual IEnumerable<Movier> CreateChild() { return null; }
        public void DoDamage(Movier other)
        {
            if (other == null)
                return;
            var dmg = Math.Min(other.hp, this.damage);
            this.hp += dmg;
            other.hp -= dmg;
        }
        public double Dist (Movier other)
        {
            if (other == null)
                return double.MaxValue;
            var cx = other.x - this.x;
            var cy = other.y - this.y;
            return Math.Sqrt(cx*cx + cy*cy);
        }
        protected Movier Nearest(IEnumerable<Movier> others, Func<Movier, bool> filter)
        {
            return others.Where(filter).OrderBy(Dist).FirstOrDefault();
        }
        protected (double dx, double dy) Shift(Movier other, double shift)
        {
            if (other == null)
                return (0,0);
            var dist = Dist(other);
            if (dist < 0.00001)
                return (0,0);
            var cx = other.x - this.x;
            var cy = other.y - this.y;
            shift = Math.Min(shift, dist);
            return (shift*cx/dist, shift*cy/dist);
        }


    }
}