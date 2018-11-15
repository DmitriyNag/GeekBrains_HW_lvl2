using System;
using System.Drawing;

namespace MyGame
{
    class SmallStar : Star
    {
        protected Bitmap pt = new Bitmap(1, 1);
        /// <summary>
        /// Конструктор, создаем мулую звезду. скорость звезды будет определена рандомно от minspeed до maxspeed
        /// </summary>
        /// <param name="pos">точка, место положения звезды</param>
        /// <param name="dir">вектор, направление движения звезды</param>
        /// <param name="minspeed">минимальная скорость движения звезды</param>
        /// <param name="maxspeed">макиамльная скорость движения звезды</param>
        public SmallStar(Point pos, Point dir, int minspeed, int maxspeed) : base(pos, dir, minspeed, maxspeed)
        {
        }
        /// <summary>
        /// Отрисовываем звезду малого размера
        /// </summary>
        public override void Draw()
        {
            pt.SetPixel(0, 0, StarColor);
            Game.Buffer.Graphics.DrawImageUnscaled(pt, Pos.X, Pos.Y);
        }
    }

}
