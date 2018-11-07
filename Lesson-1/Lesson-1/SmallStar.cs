using System;
using System.Drawing;

namespace Lesson_1
{
    class SmallStar : Star
    {
        protected Bitmap pt = new Bitmap(1, 1);
        public SmallStar(Point pos, Point dir) : base(pos, dir)
        {
            r = new Random();
        }
        public override void Draw()
        {
            pt.SetPixel(0, 0, StarColor);
            Game.Buffer.Graphics.DrawImageUnscaled(pt, Pos.X, Pos.Y);
        }
    }

}
