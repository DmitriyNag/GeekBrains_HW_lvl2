using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_1
{
    /// <summary>
    /// Класс объекта Аптечка
    /// </summary>
    internal class Medkit : ActiveObj
    {
        
        /// <summary>
        /// Параметр, HP которое аптечка восстанавливает
        /// </summary>
        public int HP { get; private set; }
        /// <summary>
        /// переменнне для удобства расчета отрисовки креста аптечки
        /// </summary>
        private int x1,x2,y1,y2 = default(int);
        /// <summary>
        /// Конструктор объекта Аптечка
        /// </summary>
        /// <param name="pos">Позиция Аптечки</param>
        /// <param name="size">Размер Аптечки</param>
        /// <param name="hp">HP Аптечки</param>
        /// <exception cref="GameObjectException">Исключение, в случае некорректных параметров создания объекта, HP не может быть меньше или равна 0</exception>
        public Medkit(Point pos, Point size, int hp) : base(pos, new Point(0,0), size)
        {
            HP = (hp > 0) ? hp : throw new GameObjectException("error creating game object, size can't be 0 or negative");
            OnLog($"Medkit added in X: {Pos.X} Y: {Pos.Y}");
        }
        /// <summary>
        /// Отрисовка аптечки
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.White, Pos.X, Pos.Y, Size.X, Size.Y);
            x1 = Pos.X + 3 * Size.X / 8;
            y1 = Pos.Y + Size.Y / 8;
            x2 = Size.X / 4;
            y2 = 3 * Size.Y / 4;
            Game.Buffer.Graphics.FillRectangle(Brushes.DarkRed, x1, y1, x2, y2);
            x1 = Pos.X + Size.X / 8;
            y1 = Pos.Y + 3* Size.Y / 8;
            x2 = 3 * Size.X / 4;
            y2 = Size.Y / 4;
            Game.Buffer.Graphics.FillRectangle(Brushes.DarkRed, x1, y1, x2, y2);
        }
        /// <summary>
        /// Обновление состояния Аптечки
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update()
        {
        }
    }
}
