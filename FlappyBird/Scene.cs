using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Scene : IObservable
    {
        Random random = new Random();
        Render render;
        Wall[] walls;
        Bird bird;
        List<IObserver> observers = new List<IObserver>();
        bool gameStarted = false;
        internal int width { get; private set; }
        internal int height { get; private set; }
        internal int wallAmount { get; private set; }
        internal Scene(int width, int height, int wallAmount)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width + 1, height + 1);
            this.width = width;
            this.height = height;
            this.wallAmount = wallAmount;
            render = new Render(this);
            bird = new Bird(this, 0, width / (2 * wallAmount), wallAmount * width / (height / 2));
            AddObserver(bird);
            walls = new Wall[wallAmount];
            for(int x = 0; x < walls.Length; x++)
            {
                walls[x] = new Wall(this, x * width / wallAmount, random.Next(0, height), width / (10 * wallAmount), 2, random);
                AddObserver(walls[x]);
            }
            
        }
        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }
        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }
        public void Notify()
        {
            for(int i = 0; i < observers.Count; i++)
            {
                observers[i].Update();
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
            FillRenderScene();
            render.RenderScene();
        }
        internal void Update(ref bool IsGameAlive)
        {
            FillRenderScene();
            render.RenderScene();
            Notify();
            //render.DumbRender();
            //IsGameAlive = !CheckBirdCollision();
            Render.CalculateDeltaTime();
            render.WriteDeltaTime();
            //(x3 < x1 && x1 < x4) || (x3 < x2 && x2 < x4)
        }
    }
}
