using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Render
    {
        static internal float deltaTime { get; private set; }
        static int t0 = DateTimeOffset.UtcNow.Millisecond;
        static int t1 = 0;
        Scene scene;
        char[,] currScene;
        char[,] prevScene;
        static internal void CalculateDeltaTime()
        {
            t0 = DateTimeOffset.UtcNow.Millisecond;
            if (t0 < t1) t0 += 1000;
            deltaTime = (t0 - t1);
            deltaTime /= 1000f;
            if (t0 >= 1000) t0 -= 1000;
            t1 = t0;
        }
        internal void SetPixel(int x, int y, char symbol)
        {
            if (!IsCordValid(x, y)) return;
            currScene[x, y] = symbol;
        }
        internal Render(Scene scene)
        {
            Console.CursorVisible = false;
            this.scene = scene;
            currScene = new char[scene.width, scene.height];
            prevScene = new char[scene.width, scene.height];
        }
        internal void DumbRender()
        {
            for (int x = 0; x < scene.width; x++)
            {
                for (int y = 0; y < scene.height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(currScene[x, y]);
                }
            }
        }
        internal void RenderScene()
        {
            for(int x = 0; x < scene.width; x++)
            {
                for(int y = 0; y < scene.height; y++)
                {
                    if (currScene[x, y] != prevScene[x, y])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(currScene[x, y]);
                    }
                    prevScene[x, y] = currScene[x, y];
                    currScene[x, y] = ' ';
                }
            }
        }
        internal void WriteDeltaTime()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(deltaTime);
        }
        bool IsCordValid(int x, int y)
        {
            if (x >= scene.width || x < 0 || y >= scene.height || y < 0) return false;
            return true;
        }
    }
}
