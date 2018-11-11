using System.Drawing;

namespace Lesson_1
{
    class Asteroid : BaseObject
    {
        protected Image image;

        public Asteroid(Point pos, Point dir) : base(pos, dir)
        {
            image = Image.FromFile("../../aster.gif");
            Size = image.Size;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }

}
