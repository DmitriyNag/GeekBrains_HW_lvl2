using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_1
{
    /// <summary>
    /// Класс Корабль
    /// </summary>
    internal class Ship : ActiveObj
    {
        /// <summary>
        /// HP корабля
        /// </summary>
        public int Energy { get; private set; }
        /// <summary>
        /// Событие смерти корабля
        /// </summary>
        public event Action? MessageDie;
        /// <summary>
        /// Конструктор Корабля
        /// </summary>
        /// <param name="pos">Позиция Корабля</param>
        /// <param name="dir">Направление движения корабля и скорость</param>
        /// <param name="size">Размер корабля</param>
        /// <param name="energy">HP корабля, не менее 1</param>
        public Ship(Point pos, Point dir, Point size,int energy) : base(pos, dir, size)
        {
            Energy = energy > 0 ? energy : 1;
            OnLog($"Ship added in X: {Pos.X} Y: {Pos.Y}");
        }
        /// <summary>
        /// Изменение HP Корабля
        /// </summary>
        /// <param name="n"></param>
        public void EnergyChng(int n) => Energy += n;
        /// <summary>
        /// Отрисовка Корабля
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.X, Size.Y);
            Game.Buffer.Graphics.DrawString("Energy: " + Energy, new Font(FontFamily.GenericSansSerif, 20), Brushes.White, 0, 0);
        }
        /// <summary>
        /// Обновление состояния корабля
        /// </summary>
        public override void Update()
        {
        }
        /// <summary>
        /// Движение корабля вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y -= Dir.Y;
        }
        /// <summary>
        /// Движение корабля вниз
        /// </summary>
        public void Down()
        {
            if(Pos.Y <Game.Height) Pos.Y+= Dir.Y;
        }
        /// <summary>
        /// Смерть корабля
        /// </summary>
        public void Die()
        {
            OnLog($"Ship destroed in X: {Pos.X} Y: {Pos.Y}");
            MessageDie?.Invoke();
        }
    }
}
