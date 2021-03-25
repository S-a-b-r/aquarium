using System;
using System.Collections.Generic;
using System.Linq;

namespace aquarium
{
    class Aquarium
    {
        Random rnd = new Random();

        const int LEFT = 0;
        const int RIGHT = 79;
        const int TOP = 0;
        const int BOTTOM = 24;

        List<Movier> objects = new List<Movier>();

        public void AddFood()
        {
            objects.Add(new Food() {
                x = rnd.Next(LEFT,RIGHT),
                y = TOP,
                speed = 0.5,
                size = 0.5,
                hp = 5,
                OnSpawn = AquariumObjectWhantToSpawn
            });
        }

        public void AddFish()
        {
            objects.Add(new Fish() {
                x = rnd.Next(LEFT,RIGHT),
                y = rnd.Next(TOP,BOTTOM),
                speed = 0.7,
                size = 1,
                hp = 5,
                damage = 1,
                OnSpawn = AquariumObjectWhantToSpawn
            });
        } 

        void AquariumObjectWhantToSpawn(Movier obj)
        {
            if (obj == null) return;
            var child = obj.CreateChild();
            if (child == null) return;
            objects.AddRange(child);
        }

        public void Update()
        {
            //движение
            for (int i=0; i<objects.Count; i++)
            {
                var obj = objects[i];

                (double dx, double dy) = obj.GetNextMove(objects);
                Collision c = Collision.None;
                if (obj.x + dx < LEFT || obj.x + dx > RIGHT) { dx = 0; c = Collision.Vertical; }
                if (obj.y + dy < TOP || obj.y + dy > BOTTOM) { dy = 0; c = Collision.Horizontal; }
                obj.Move(dx,dy,c);
            }

            //касания
            foreach (var obj1 in objects)
                foreach (var obj2 in objects)
                    if (obj1 != obj2 && obj1.Dist(obj2) < obj1.size + obj2.size)
                        obj1.Touch(obj2);

            //удаление
            for (int i = objects.Count - 1; i >= 0; i--)
                if (objects[i].hp <= 0)
                    objects.RemoveAt(i);
        }
        public void Draw()
        {
            Console.Clear();
            foreach(var obj in objects)
                obj.Draw();
        }
    }
}