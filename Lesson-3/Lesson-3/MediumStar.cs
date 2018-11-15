using System;
using System.Drawing;

namespace MyGame
{
    class MediumStar : Star
    {
        /// <summary>
        /// Конструктор звезда среднего размера. скорость звезды будет определена рандомно от minspeed до maxspeed
        /// </summary>
        /// <param name="pos">место положения звезды</param>
        /// <param name="dir">скорость и направление движения звезды</param>
        public MediumStar(Point pos, Point dir) : base(pos, dir)
        {
        }
        /// <summary>
        /// Отрисовываем звезду среднего размера
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(new Pen(StarColor), Pos.X, Pos.Y, 1, 1);
        }
    }

}
