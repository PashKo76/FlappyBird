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
        internal Bird(ref Update update, Scene scene, float y, float x, float fallAceleration) : base(ref update, scene, x, y)
        {
            this.fallAceleration = fallAceleration;
            fallSpeed = 0;
            Thread thread = new Thread(Key);
            thread.Start();
        }
        internal override void Update()
        {
            if (fall)
            {
                fallSpeed += fallAceleration * Render.deltaTime;
            }
            else
            {
                fallSpeed = -fallAceleration / 2;
            }
            y += fallSpeed * Render.deltaTime;
            fall = true;
            
        }
        void Key()
        {
            while (true)
            {
                if (Console.ReadKey().Key != ConsoleKey.UpArrow) continue;
                fall = false;
            }
        }
    }
}
