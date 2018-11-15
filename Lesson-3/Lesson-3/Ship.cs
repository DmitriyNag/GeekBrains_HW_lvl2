using System;
using System.Drawing;

namespace MyGame
{
    class Ship : BaseObject
    {
        protected Image image;
        public int Energy { set; get; } = Game.shipMaxEnergy;
        public int Score { get; set; }
        public static event Message ShipDie;
        public Point ShipNouse
        {
            get { return new Point(Pos.X + Size.Width, Pos.Y + Size.Height/2); }
        }

        public Ship(Point pos, Point dir) : base(pos, dir)
        {
            image = Image.FromFile("../../Rocket.gif");
            Size = image.Size;
        }
        public override void Draw()
        {
            //Game.Buffer.Graphics.FillEllipse(Brushes.Aqua,Pos.X,Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }

        public override void Update()
        {

        }

        public void Up()
        {
            if (Pos.Y > 0)
                Pos.Y -= Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height)
                Pos.Y += Dir.Y;
        }
        public void Right()
        {
            if (Pos.X < Game.Width-Size.Width)
                Pos.X += Dir.X;
        }
        public void Left()
        {
            if (Pos.X > Size.Width)
                Pos.X -= Dir.X;
        }

        internal void ScoreUp(int s)
        {
            Score += s;
        }
        internal void EnergyLow(int v)
        {
                Energy -= v;
        }
        internal void EnergyHigh(int v)
        {
            Energy += v;
        }

        internal void Die()
        {
            ShipDie?.Invoke();
        }
    }

}
