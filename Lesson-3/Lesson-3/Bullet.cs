using System.Drawing;
namespace MyGame
{
    class Bullet : BaseObject
    {
        /// <summary>
        /// Создаем объект пуля
        /// </summary>
        /// <param name="pos">место полоения пули</param>
        /// <param name="dir">направление движения пуди и скорость</param>
        public Bullet(Point pos, Point dir) : base(pos, dir)
        {
            Size size = new Size(1, 4);
        }
        /// <summary>
        /// Отрисовываем пулю
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Обнолвяем положение пули
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 20;
            if (Pos.X > Game.Width)
            {
                Pos.X = 0;
            }
        }
        /// <summary>
        /// Регенерируем пулю в начало экрана
        /// </summary>
        public void Renew()
        {
            Pos.X = 0;
        }
    }

}
