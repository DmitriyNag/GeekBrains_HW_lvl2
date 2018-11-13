using System;
using System.Drawing;

namespace MyGame
{
    class Star : BaseObject
    {
        static Random rnd = new Random();
        protected Color StarColor { get; set; }
        protected int Speed { get; set; }

        /// <summary>
        /// Конструктор, создаем звезду. скорость звезды будет определена рандомно от minspeed до maxspeed
        /// </summary>
        /// <param name="pos">место располоения звезды</param>
        /// <param name="dir">направление движени и скорость</param>
        /// <param name="minspeed"> минимально возможная скорость звезды</param>
        /// <param name="maxspeed"> максимально возможная скорость звезды</param>
        public Star(Point pos, Point dir,int minspeed, int maxspeed) : base(pos, dir)
        {
            SelectStarColor();
            Speed = rnd.Next(minspeed, maxspeed);
        }
        /// <summary>
        /// Отрисовываем звезду
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
        /// <summary>
        /// обновляем положение звезды
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + 1;
                Pos.Y = rnd.Next(0, Game.Height);
                //Dir.X = rnd.Next(MinSpeed, MaxSpeed);
                SelectStarColor();
            }
        }
        /// <summary>
        /// Выбираем цвет для звезды
        /// </summary>
        private void SelectStarColor()
        {
            
            switch (rnd.Next(1, 7))
            {
                case 1:
                    StarColor = Color.AliceBlue;
                    break;
                case 2:
                    StarColor = Color.AntiqueWhite;
                    break;
                case 3:
                    StarColor = Color.RoyalBlue;
                    break;
                case 4:
                    StarColor = Color.CadetBlue;
                    break;
                case 5:
                    StarColor = Color.NavajoWhite;
                    break;
                case 6:
                    StarColor = Color.LightGoldenrodYellow;
                    break;
                default:
                    StarColor = Color.White;
                    break;

            }
            //Console.WriteLine(StarColor);
        }
    }

}
