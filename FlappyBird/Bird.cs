using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Bird : GameObject
    {
        bool fall = true;
        float fallAceleration;
        float fallSpeed;
        float jumpSpeed;
        internal Bird(Scene scene, float y, float x, float fallAceleration) : base(scene, x, y)
        {
            this.fallAceleration = fallAceleration;
            fallSpeed = 0;
            jumpSpeed = -fallAceleration / 2;
            Thread thread = new Thread(() => Key(ref fall));
            thread.Start();
        }
        public override void Update()
        {
            if (fall)
            {
                fallSpeed += fallAceleration * Render.deltaTime;
            }
            else
            {
                fallSpeed = jumpSpeed;
            }
            y += fallSpeed * Render.deltaTime;
            fall = true;
        }
        void Key(ref bool Bool)
        {
            while (true)
            {
                if (Console.ReadKey().Key != ConsoleKey.UpArrow) continue;
                Bool = false;
            }
        }
    }
}
