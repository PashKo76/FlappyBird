using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Scene
    {
        Random random = new Random();
        Render render;
        Wall[] walls;
        Bird bird;
        Update update = () => { };
        bool gameStarted = false;
        internal int width { get; private set; }
        internal int height { get; private set; }
        internal int wallAmount { get; private set; }
        internal Scene(int width, int height, int wallAmount)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            this.width = width;
            this.height = height;
            this.wallAmount = wallAmount;
            render = new Render(this);
            bird = new Bird(ref update, this, height / 8, width / (2 * wallAmount), wallAmount * width / (height / 2));
            walls = new Wall[wallAmount];
            for(int x = 0; x < walls.Length; x++)
            {
                walls[x] = new Wall(ref update, this, x * width / wallAmount, random.Next(0, height), width/(10*wallAmount), 1, random);
            }
        }
        bool CheckBirdCollision()
        {
            if (bird.RoundY() >= height || bird.RoundY() < 0) return true;
            for(int i = 0; i < walls.Length; i++)
            {
                if (bird.RoundX() != walls[i].RoundX())
                {
                    continue;
                }
                if(bird.RoundY() > walls[i].RoundY() + walls[i].holeSize || bird.RoundY() < walls[i].RoundY() - walls[i].holeSize)
                {
                    return true;
                }
            }
            return false;
        }
        void FillRenderScene()
        {
            render.SetPixel(bird.RoundX(), bird.RoundY(), '@');
            for(int i = 0; i < walls.Length; i++)
            {
                for(int y = 0; y < height; y++)
                {
                    if(y > walls[i].RoundY() + walls[i].holeSize || y < walls[i].RoundY() - walls[i].holeSize)
                    {
                        render.SetPixel(walls[i].RoundX(), y, '\u2588');
                    }
                }
            }
        }
        internal void Start()
        {
            FillRenderScene();
            render.RenderScene();
            Console.SetCursorPosition(width / 2, height / 2);
            Console.WriteLine("Wait 2 seconds");
            Thread.Sleep(2000);
        }
        internal void Update(ref bool IsGameAlive)
        {
            FillRenderScene();
            render.RenderScene();
            update.Invoke();
            IsGameAlive = !CheckBirdCollision();
            Render.CalculateDeltaTime();
        }
    }
}
