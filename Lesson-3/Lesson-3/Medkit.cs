using System;
using System.Drawing;

namespace MyGame
{
    class Medkit : BaseObject
    {
        protected int Speed { get; set; }
        protected Image image;

        public Medkit(Point pos, Point dir) : base(pos, dir)
        {
            image = Image.FromFile("../../Medkit.gif");
            Size = image.Size;
        }
        /// <summary>
        /// Отрисовываем звезду
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
        /// <summary>
        /// обновляем положение звезды
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0)
                Regenerate();
        }

        public void Regenerate()
        {
            Pos.X = 3 * Game.Width;
            Pos.Y = rnd.Next(0, Game.Height);
            Dir = new Point(rnd.Next(10, 20), 0);
        }
    }

}
