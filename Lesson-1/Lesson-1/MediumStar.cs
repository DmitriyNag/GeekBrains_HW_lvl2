using System;
using System.Drawing;

namespace Lesson_1
{
    class MediumStar : Star
    {
        public MediumStar(Point pos, Point dir) : base(pos, dir)
        {
            r = new Random();
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(new Pen(StarColor), Pos.X, Pos.Y, 1, 1);
        }
    }

}
