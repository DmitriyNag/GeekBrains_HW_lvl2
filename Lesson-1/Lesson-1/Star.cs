using System;
using System.Drawing;

namespace Lesson_1
{
    class Star : BaseObject
    {
        protected Color StarColor { get; set; }
        protected Random r = new Random();
        protected int Distance {get;set;}

        public Star(Point pos, Point dir) : base(pos, dir)
        {
            //Size = new Size(1, 1);
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
                Pos.X = Game.Width + 1;
                Pos.Y = r.Next(0, Game.Height);
                Dir.X = r.Next(3, 15);
                Dir.Y = 0;
            }
        }

        private void SelectStarColor()
        {
            switch (r.Next(1, 10))
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
                    StarColor = Color.BlueViolet;
                    break;
                case 5:
                    StarColor = Color.Violet;
                    break;
                case 6:
                    StarColor = Color.CadetBlue;
                    break;
                case 7:
                    StarColor = Color.NavajoWhite;
                    break;
                case 8:
                    StarColor = Color.LightYellow;
                    break;
                case 9:
                    StarColor = Color.Yellow;
                    break;
                default:
                    StarColor = Color.White;
                    break;
            }

        }
    }

}
