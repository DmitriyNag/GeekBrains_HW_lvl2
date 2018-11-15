using System;
using System.Drawing;

namespace MyGame
{
    class Medkit : BaseObject
    {
        protected Image image;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
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
        /// <summary>
        /// Заново создать аптечку в игре
        /// </summary>
        public void Regenerate()
        {
            Pos.X = 3 * Game.Width;
            Pos.Y = rnd.Next(0, Game.Height);
            Dir = new Point(rnd.Next(10, 20), 0);
        }
    }

}
