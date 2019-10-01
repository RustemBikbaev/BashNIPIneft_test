using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp_test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(700, 400); //задаем размер формы
          
        }

        public bool peresech(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4) // функция определяющая пересекаются-ли 2 отрезка 
        {

            double Ua, Ub, numerator_a, numerator_b, denominator;

            denominator = (y4 - y3) * (x1 - x2) - (x4 - x3) * (y1 - y2);
            if (denominator == 0)
            {
                if ((x1 * y2 - x2 * y1) * (x4 - x3) - (x3 * y4 - x4 * y3) * (x2 - x1) == 0 && (x1 * y2 - x2 * y1) * (y4 - y3) - (x3 * y4 - x4 * y3) * (y2 - y1) == 0)
                    return true;
                else return false;
            }
            else
            {
                numerator_a = (x4 - x2) * (y4 - y3) - (x4 - x3) * (y4 - y2);
                numerator_b = (x1 - x2) * (y4 - y2) - (x4 - x2) * (y1 - y2);
                Ua = numerator_a / denominator;
                Ub = numerator_b / denominator;
                if((Ua >= 0 )&& (Ua <= 1 ) && (Ub >= 0 )&& (Ub <= 1 ))
                    return true;
                else return false;
            }           
        }


        public void prinadlejnost(int Num_point, Point[] points , Point[] rectangle, List<int> in_rectangle) // проверка принадлежности отрезка прямоугольнику 
        {
            for (int i = 0; i < Num_point - 1; i++) 
            {
                bool peresech_up = peresech(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, rectangle[0].X, rectangle[0].Y, rectangle[1].X, rectangle[0].Y);
                bool peresech_down = peresech(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, rectangle[0].X, rectangle[1].Y, rectangle[1].X, rectangle[1].Y);
                bool peresech_left = peresech(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, rectangle[0].X, rectangle[0].Y, rectangle[0].X, rectangle[1].Y);
                bool peresech_right = peresech(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, rectangle[1].X, rectangle[0].Y, rectangle[1].X, rectangle[1].Y);
                bool line_in_rect = false;
                // лежит ли отрезок внитри прямоугольника
                if (((points[i].X >= rectangle[0].X) && (points[i].X <= rectangle[1].X) && (points[i].Y >= rectangle[0].Y) && (points[i].Y <= rectangle[1].Y)) && ((points[i + 1].X >= rectangle[0].X) && (points[i + 1].X <= rectangle[1].X) && (points[i + 1].Y >= rectangle[0].Y) && (points[i + 1].Y <= rectangle[1].Y)))
                {
                    line_in_rect = true;
                }


                if (peresech_up || peresech_down || peresech_left || peresech_right || line_in_rect)
                {
                    in_rectangle.Add(i); // добавление удовлетваряющих  нас точек отрезков в массив
                    in_rectangle.Add(i + 1);
                }
            }
        }

        private void DrawLinesPoint(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            Pen penG = new Pen(Color.Green, 2); //ручки для отрисовки
            Pen penR = new Pen(Color.Red, 2);
            Pen penB = new Pen(Color.Blue, 2);

            const int Num_point = 20; // количество точек задающих отрезки
            Random rand = new Random();

            Point[] points = new Point[Num_point]; // массив точек задающих отрезки
            Point[] rectangle = { new Point(100, 100), new Point(300, 200) }; //верхня левая и нихняя правая точка прямоугольника
            List<int> in_rectangle = new List<int>(); // список точек пересекающих или попадающих в прямоугольник 

            for (int i = 0; i < Num_point; i++) // задание рандомных точек для прямых
            {
                points[i] = new Point(rand.Next(this.Size.Width), rand.Next(this.Size.Height));

            }

            prinadlejnost(Num_point, points, rectangle, in_rectangle);

            g.DrawLines(penG, points); // отрисовка отрезков (зеленым)          

            for (int i = 0; i < in_rectangle.Count; i=i+2) //отрисовки линий пересекающих прямоугольник (красных)
            {
                g.DrawLine(penR, points[in_rectangle[i]].X, points[in_rectangle[i]].Y, points[in_rectangle[i] + 1].X, points[in_rectangle[i] + 1].Y);
            }

            g.DrawLine(penB, rectangle[0].X, rectangle[0].Y, rectangle[1].X, rectangle[0].Y); // отрисовка прямоугольника (синим)
            g.DrawLine(penB, rectangle[0].X, rectangle[1].Y, rectangle[1].X, rectangle[1].Y);
            g.DrawLine(penB, rectangle[0].X, rectangle[0].Y, rectangle[0].X, rectangle[1].Y);
            g.DrawLine(penB, rectangle[1].X, rectangle[0].Y, rectangle[1].X, rectangle[1].Y);

        }
    }
}
