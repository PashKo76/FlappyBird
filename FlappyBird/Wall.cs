using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Wall : GameObject
    {
        Random rand;
        float speed;
        internal int holeSize { get; private set; }
        public Wall(Scene scene, float x, float y, float speed, int holeSize, Random random) : base(scene, x, y)
        {
            this.speed = speed;
            this.holeSize = holeSize;
            rand = random;
        }
        public override void Update()
        {
            x -= speed * Render.deltaTime;
            x = Distance(x, scene.width, 0);
        }
        float Distance(float Curr, float Max, float Min)
        {
            if (Curr >= Max) return Curr - Max;
            else if (Curr < Min) return Curr + Max;
            else
            {
                y = rand.Next(holeSize, scene.height - holeSize);
                return Curr;
            }
        }
    }
}
