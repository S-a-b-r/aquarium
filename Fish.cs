using System;
using System.Collections.Generic;
using System.Linq;

namespace aquarium
{
    class Fish : Movier
    {
        static Random rnd = new Random();
        const int EYE_RANGE = 10;
        const int SPAWN_HP = 20;
        double targetx = rnd.Next(-20,20);
        double targety = rnd.Next(-10,10);

        public override void Draw()
        {
            ConsoleEx.Print((int)x, (int)y, "#", ConsoleColor.Yellow);
        }
        public override void Touch(Movier other)
        {
            if (other != null && other is Food && !other.ground)
                DoDamage(other);
        }
        public override void Move(double dx, double dy, Collision c)
        {
            x += dx;
            y += dy;
            targetx -= dx;
            targety -= dy;
            if (Math.Abs(targetx) <= 0.00001) targetx = rnd.Next(-20,20);
            if (Math.Abs(targety) <= 0.00001) targety = rnd.Next(-10,10);
            if (c == Collision.Vertical) targetx = -targetx;
            if (c == Collision.Horizontal) targety = -targety;

            if (hp > SPAWN_HP && OnSpawn != null)
                OnSpawn(this);
        }
        public override (double dx, double dy) GetNextMove(IEnumerable<Movier> others)
        {
            //var enemy = Nearest(others, obj => obj is BadFish && Dist(obj) < EYE_RANGE);
            //if (enemy == null)
            //    return Shift(enemy, -speed);

            var target = Nearest(others, obj => obj is Food && !obj.ground && Dist(obj) < EYE_RANGE);
            if (target == null)
                target = new Movier() {x = this.x + targetx, y = this.y + targety};
            return Shift(target, speed);
        }
        public override IEnumerable<Movier> CreateChild()
        {
            this.hp /= 2;

            var child = new Fish()
            {
                x = this.x,
                y = this.y,
                size = this.size,
                speed = this.speed * 0.7,
                hp = this.hp,
                damage = this.damage,
                ground = this.ground,
                OnSpawn = this.OnSpawn
            };

            return new Fish[] { child };
        }
    }
}