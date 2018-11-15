using System.Drawing;
using System;

namespace MyGame
{
    class Asteroid : BaseObject
    {
        protected Image image;

        /// <summary>
        /// конструктор Астероида, 
        /// </summary>
        /// <param name="pos">точка место положение астероида</param>
        /// <param name="dir">вектор направление движения астероида</param>
        public Asteroid(Point pos, Point dir) : base(pos, dir)
        {
            image = Image.FromFile("../../aster.gif");
            Size = image.Size;
        }
        /// <summary>
        /// отриосываем астериод
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
        /// <summary>
        /// обновляем положение астероида
        /// </summary>
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0 - Size.Width)
            {
                Pos.X = Game.Width + Size.Width;
            }
            if (Pos.X > Game.Width + Size.Width)
            {
                Pos.X = 0 - Size.Width;
            }
            if (Pos.Y < 0 - Size.Height)
            {
                Pos.Y = Game.Height + Size.Height;
            }
            if (Pos.Y > Game.Height + Size.Height)
            {
                Pos.Y = 0 - Size.Height;
            }
        }
        /// <summary>
        /// Регенерируем астериод в точку на границе экрана
        /// </summary>
        public void Renew()
        {
            Pos.X = Game.Width +1;
            Pos.Y = rnd.Next(0, Game.Height);
            Dir.X = -Math.Abs(Dir.X);
        }

    }

}
