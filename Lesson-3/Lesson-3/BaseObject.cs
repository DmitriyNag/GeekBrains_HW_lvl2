using System;
using System.Drawing;

namespace MyGame
{
    abstract class BaseObject : ICollision
    {
        protected const int maxSize = 200;
        protected const int maxDir = 200;
        protected const int maxPoint = 4000;
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        static protected Random rnd = new Random();
        public delegate void Message();

        /// <summary>
        /// Интерфейс ICollision, вычисляем столкновение с другими объектами
        /// </summary>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

        public BaseObject(Point pos, Point dir, Size size): this(pos, dir)
        {
            if (size.Height > 0 && size.Width > 0 && size.Height < maxSize && size.Width < maxSize)
            {
                Size = size;
            }
            else throw new GameObjectException($"Размер объекта задан не верно - Height: {size.Height.ToString()}, Width: {size.Width.ToString()}");
        }
        public BaseObject(Point pos, Point dir)
        {
            if (Math.Abs(dir.X) < 100 && Math.Abs(dir.Y) < 100)
            {
                Dir = dir;
            }
            else throw new GameObjectException($"Скорость объекта задана не верно - по оси X: {dir.X.ToString()}, по оси Y: {dir.Y.ToString()}");

            if (Math.Abs(pos.X) < maxPoint && Math.Abs(pos.Y) < maxPoint)
            {
                Pos = pos;
            }
            else throw new GameObjectException($"Положение объекта слшком большое - по оси X: {pos.X.ToString()}, по оси Y: {pos.Y.ToString()}");
        }
        /// <summary>
        /// абстрактный метод, отрисовываем объект
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// абстрактный метод, обновляем положение объекта
        /// </summary>
        public abstract void Update();


    }

}
