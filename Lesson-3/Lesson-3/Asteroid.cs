using System.Drawing;
using System.Security.Cryptography.X509Certificates;

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
                //Dir.X = -Dir.X;
                Pos.X = Game.Width + Size.Width;
            }
            if (Pos.X > Game.Width + Size.Width)
            {
                //Dir.X = -Dir.X;
                Pos.X = 0 - Size.Width;
            }
            if (Pos.Y < 0 - Size.Height)
            {
                //Dir.Y = -Dir.Y;
                Pos.Y = Game.Height + Size.Height;
            }
            if (Pos.Y > Game.Height + Size.Height)
            {
                //Dir.Y = -Dir.Y;
                Pos.Y = 0 - Size.Height;
            }
        }
        /// <summary>
        /// Регенерируем астериод в точку на границе экрана
        /// </summary>
        public void Renew()
        {
            //Pos.X = Game.Width - Size.Width;
            Dir.X = -Dir.X;
        }

    }

}
