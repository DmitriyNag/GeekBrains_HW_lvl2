using System;
using System.Drawing;

namespace Lesson_1
{
    class Star : BaseObject
    {
        protected Color StarColor { get; set; }
        protected int MinSpeed { get; set; }
        protected int MaxSpeed { get; set; }

        public Star(Point pos, Point dir) : base(pos, dir)
        {
            SelectStarColor();
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0)
            {
                Random rnd = new Random();
                Pos.X = Game.Width + 1;
                Pos.Y = rnd.Next(0, Game.Height);
                Dir.X = rnd.Next(MinSpeed, MaxSpeed);
                SelectStarColor();
            }
        }

        private void SelectStarColor()
        {
            Random rnd = new Random();
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
            Console.WriteLine(StarColor);
        }
    }

}
