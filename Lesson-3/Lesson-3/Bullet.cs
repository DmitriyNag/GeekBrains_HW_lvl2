using System;
using System.Drawing;
namespace MyGame
{
    class Bullet : BaseObject
    {
        public delegate void BullMessage(object obj);
        public event BullMessage BulletDelete;
        /// <summary>
        /// Создаем объект пуля
        /// </summary>
        /// <param name="pos">место полоения пули</param>
        /// <param name="dir">направление движения пуди и скорость</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir,size)
        {
        }
        /// <summary>
        /// Отрисовываем пулю
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.DarkRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Обнолвяем положение пули
        /// </summary>
        public override void Update()
        {
            Pos.X+= Dir.X;
            if (Pos.X > Game.Width)
            {
                BulletDelete?.Invoke(this); // уничтожение пули
            }
        }

    }

}
