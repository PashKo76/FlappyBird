using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal abstract class GameObject : IObserver
    {
        internal float x { get; private protected set; }
        internal float y { get; private protected set; }
        internal int RoundX()
        {
            return (int)MathF.Round(x);
        }
        internal int RoundY()
        {
            return (int)MathF.Round(y);
        }
        protected Scene scene;
        internal GameObject(Scene scene, float x, float y)
        {
            this.x = x;
            this.y = y;
            this.scene = scene;
        }
        abstract public void Update();
    }
    interface IObserver
    {
        void Update();
    }
    interface IObservable
    {
        void AddObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void Notify();
    }
}
