using System.Drawing;
namespace Lesson_1
{
    public class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        //public BaseObject(Point pos, Point dir, Size size)
        //{
        //    Pos = pos;
        //    Dir = dir;
        //    Size = size;
        //}
        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
            Size = new Size(2,2);

        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.AntiqueWhite, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0)
            {
                Dir.X = -Dir.X;
                Pos.X = 0;
            }
            if (Pos.X > Game.Width - Size.Width)
            {
                Dir.X = -Dir.X;
                Pos.X = Game.Width - Size.Width;
            }
            if (Pos.Y < 0 )
            {
                Dir.Y = -Dir.Y;
                Pos.Y = 0;
            }
            if (Pos.Y > Game.Height-Size.Height)
            {
                Dir.Y = -Dir.Y;
                Pos.Y = Game.Height-Size.Height;
            }
        }   
    }

}
