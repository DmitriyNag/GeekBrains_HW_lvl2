using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public class Rect
    {
        public int Width { get; set; }
        public Color Color { get; set; }
        public Rect(int w, Color c)
        {
            Width = w;
            Color = c;
        }
    }
    
    class ArraysSort
    {
        public ObservableCollection<Rect> Array { get; private set; }
        private Random rnd;
        private int block;
        public ArraysSort()
        {
            Array = new ObservableCollection<Rect>();
            rnd = new Random();
            block = 50;
        }
        public void Add()
        {
            for (int i = 0; i < block; i++)
            {
                Array.Add(new Rect(rnd.Next(50, 1000),Colors.Black));
            }
        }
        public async void SortMyOwn()
        {
            Rect temp;
            if(Array.Count > 1)
            {
                for (int i = 0; i < Array.Count-1; i++)
                {
                    for (int j = 0; j < Array.Count-i-1; j++)
                    {
                        if (Array[j].Width > Array[j+1].Width)
                        {
                            Array[j].Color = Colors.Red;
                            temp = Array[j];
                            Array[j] = Array[j+1];
                            Array[j+1] = temp;
                            await Task.Delay (30);
                            Array[j] = new Rect(Array[j].Width, Colors.Black);
                        }
                    }
                }
            }
        }

        public override string ToString() 
        {
            string s = string.Empty;
            foreach (var item in Array)
            {
                s += item.Width + "| "+((item.Color==Colors.Red)?"R":"B")+"| ";
            }
            return s;
        }
        public bool CheckArraySort()
        {
            for (int i = 0; i < Array.Count-1; i++)
            {
                if (Array[i].Width > Array[i + 1].Width) return false;
            }
            return true;
        }
    }
}
