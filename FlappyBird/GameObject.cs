using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal abstract class GameObject
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
        internal GameObject(ref Update update, Scene scene, float x, float y)
        {
            this.x = x;
            this.y = y;
            this.scene = scene;
            update += Update;
        }
        abstract internal void Update();
    }
}
