using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace MazeRunner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Graphics gfx;
        Bitmap map;
        Random gen = new Random();

        bool drawFull = true;

        int size = 41;
        int s = 10;

        int[,] maze = 
{{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},{0,1,1,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0},{0,1,0,1,1,1,1,1,1,1,0,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0},{0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0},{0,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,0,1,1,1,0},{0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0},{0,1,1,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1,0},{0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0},{0,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,0},{0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0},{0,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,1,1,0},{0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0},{0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,1,1,0},{0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0},{0,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0},{0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0},{0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,0,1,0,1,0},{0,0,0,1,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0},{0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,0},{0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},{0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,0},{0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0},{0,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,0},{0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0},{0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0},{0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0},{0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,0},{0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0},{0,1,1,1,1,1,1,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0},{0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0},{0,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,1,1,0},{0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0},{0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,1,1,0,1,1,1,0},{0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0},{0,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,0},{0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,1,0,1,0},{0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,0},{0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0},{0,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0},{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} };



        Stack<Point> path;
        bool[,] used;

        bool found = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(s * size + 100, s * size + 100);
            map = new Bitmap(ClientSize.Width, ClientSize.Height);
            gfx = Graphics.FromImage(map);


            //var reader = new StreamReader(File.OpenRead(@"C:\test.csv"));
            //List<string> listA = new List<string>();
            //List<string> listB = new List<string>();
            //while (!reader.EndOfStream)
            //{
            //    var line = reader.ReadLine();
            //    var values = line.Split(',');

            //    listA.Add(values[0]);
            //    listB.Add(values[1]);
            //}





            used = new bool[size, size];
            //maze = new int[size, size];

            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        if (i == 0 || j == 0 || i == size - 1 || j == size - 1)
            //        {
            //            maze[j, i] = 0;
            //        }
            //        else
            //        {
            //            maze[j, i] = gen.Next(0, 2);
            //        }

            //        used[j, i] = false;


            //    }
            //}

            //maze[size - 3, size - 1] = 1; //clear end
            //maze[3, 0] = 1; //clear beginning


            path = new Stack<Point>();
            path.Push(new Point(0, 27)); //start point
            used[0, 27] = true;



        }

        private void Form1_Shown(object sender, EventArgs e)
        {



        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //gfx.Clear(BackColor);
            //check surroundings
            //if found an empty, push that spot
            //if none, pop until surrounding shows a empty non used spot

            //x + 1, y || x, y + 1 || x - 1, y || x, y - 1

            //bonus 4: x + 1, y + 1 || x - 1, y + 1 || x - 1, y - 1 || x + 1, y - 1





            //
            if (path.Peek().X + 1 < size && maze[path.Peek().X + 1, path.Peek().Y] == 1 && !used[path.Peek().X + 1, path.Peek().Y])
            {
                used[path.Peek().X + 1, path.Peek().Y] = true;
                path.Push(new Point(path.Peek().X + 1, path.Peek().Y));

            }//
            else if (path.Peek().Y - 1 >= 0 && maze[path.Peek().X, path.Peek().Y - 1] == 1 && !used[path.Peek().X, path.Peek().Y - 1])
            {
                used[path.Peek().X, path.Peek().Y - 1] = true;
                path.Push(new Point(path.Peek().X, path.Peek().Y - 1));

            }
            else if (path.Peek().Y + 1 < size && maze[path.Peek().X, path.Peek().Y + 1] == 1 && !used[path.Peek().X, path.Peek().Y + 1])
            {
                used[path.Peek().X, path.Peek().Y + 1] = true;
                path.Push(new Point(path.Peek().X, path.Peek().Y + 1));

            }//
            else if (path.Peek().X - 1 >= 0 && maze[path.Peek().X - 1, path.Peek().Y] == 1 && !used[path.Peek().X - 1, path.Peek().Y])
            {
                used[path.Peek().X - 1, path.Peek().Y] = true;
                path.Push(new Point(path.Peek().X - 1, path.Peek().Y));

            }//
            //
            //else if (path.Peek().X + 1 < size && path.Peek().Y + 1 < size && maze[path.Peek().X + 1, path.Peek().Y + 1] == 1 && !used[path.Peek().X + 1, path.Peek().Y + 1]) //diagonals
            //{
            //    used[path.Peek().X + 1, path.Peek().Y + 1] = true;
            //    path.Push(new Point(path.Peek().X + 1, path.Peek().Y + 1));

            //}//
            //else if (path.Peek().Y + 1 < size && path.Peek().X - 1 >= 0 && maze[path.Peek().X - 1, path.Peek().Y + 1] == 1 && !used[path.Peek().X - 1, path.Peek().Y + 1])
            //{
            //    used[path.Peek().X - 1, path.Peek().Y + 1] = true;
            //    path.Push(new Point(path.Peek().X - 1, path.Peek().Y + 1));

            //}//
            //else if (path.Peek().Y - 1 >= 0 && path.Peek().X - 1 >= 0 && maze[path.Peek().X - 1, path.Peek().Y - 1] == 1 && !used[path.Peek().X - 1, path.Peek().Y - 1])
            //{
            //    used[path.Peek().X - 1, path.Peek().Y - 1] = true;
            //    path.Push(new Point(path.Peek().X - 1, path.Peek().Y - 1));

            //}
            //else if (path.Peek().X + 1 < size && path.Peek().Y - 1 >= 0 && maze[path.Peek().X + 1, path.Peek().Y - 1] == 1 && !used[path.Peek().X + 1, path.Peek().Y - 1])
            //{
            //    used[path.Peek().X + 1, path.Peek().Y - 1] = true;
            //    path.Push(new Point(path.Peek().X + 1, path.Peek().Y - 1));

            //}
            else //NOT FOUND, GO BACK
            {
                path.Pop();
            }


            if (path.Count == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show(":(");
                Application.Exit();
            }

            if (path.Count != 0 && path.Peek() == new Point(40, 17))
            {
                found = true;
            }







            draw();
            //drawPartial();
            bitBox.Image = map;

            if (found) timer1.Enabled = false;
        }


        private void draw()
        {

            int dx = 20;
            int dy = 20;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    if (
                        (
                            (j == path.Peek().X && i == path.Peek().Y)
                            || (j == path.Peek().X + 1 && i == path.Peek().Y)
                            || (j == path.Peek().X - 1 && i == path.Peek().Y)
                            || (j == path.Peek().X && i == path.Peek().Y + 1)
                            || (j == path.Peek().X && i == path.Peek().Y - 1)
                            || (j == path.Peek().X + 1 && i == path.Peek().Y + 1)
                            || (j == path.Peek().X - 1 && i == path.Peek().Y + 1)
                            || (j == path.Peek().X - 1 && i == path.Peek().Y - 1)
                            || (j == path.Peek().X + 1 && i == path.Peek().Y - 1)
                         ) || drawFull
                        )
                    {
                        if (maze[j, i] == 0)
                        {
                            gfx.FillRectangle(Brushes.Black, dx, dy, s, s);
                        }
                        else
                        {
                            gfx.FillRectangle(Brushes.Blue, dx, dy, s, s);
                        }

                        if (path.Contains(new Point(j, i)))
                        {


                            if (found)
                            {
                                gfx.FillRectangle(Brushes.Gold, dx, dy, s, s);
                            }
                            else
                            {
                                gfx.FillRectangle(Brushes.Red, dx, dy, s, s);
                            }

                        }
                        else if (used[j, i])
                        {
                            gfx.FillRectangle(Brushes.Green, dx, dy, s, s);
                        }
                    }


                    dx += s;
                }
                dx = 20;
                dy += s;
            }









        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            map = new Bitmap(ClientSize.Width, ClientSize.Height);
            gfx = Graphics.FromImage(map);
        }
    }
}
