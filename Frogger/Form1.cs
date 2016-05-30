using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Frogger
{
    public partial class Form1 : Form
    {
        readonly int QUARTER_Y;
        readonly List<PictureBox> cars = new List<PictureBox>(32);
        readonly Random random = new Random();
        readonly int width;
        readonly int height;

        int surviveTime = 0;

        public Form1()
        {
            InitializeComponent();

            width = this.Size.Width;
            height = this.Size.Height;
            QUARTER_Y = height / 4;
            int tempY = 0;
            for (int i = 0; i < 4; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Image = imgs.Images[0];
                pb.Size = new Size(32, 32);
                pb.Location = new Point(0, tempY);
                tempY += QUARTER_Y;
                this.Controls.Add(pb);
                cars.Add(pb);
            }
            pbFrog.Image = imgs.Images[1];

            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pbFrog.Location = new Point(pbFrog.Location.X, pbFrog.Location.Y - 5);
                    break;
                case Keys.S:
                    pbFrog.Location = new Point(pbFrog.Location.X, pbFrog.Location.Y + 5);
                    break;
                case Keys.A:
                    pbFrog.Location = new Point(pbFrog.Location.X - 10, pbFrog.Location.Y);
                    break;
                case Keys.D:
                    pbFrog.Location = new Point(pbFrog.Location.X + 8, pbFrog.Location.Y);
                    break;
            }
        }

        int counter = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            PictureBox item;
            for (int i = 0; i < cars.Count(); i++)
            {
                item = cars[i];
                item.Location = new Point(item.Location.X + random.Next(0, 20), item.Location.Y);
                if (item.Location.X > width)
                {
                    cars.RemoveAt(i--);
                }
                if (item.Bounds.IntersectsWith(pbFrog.Bounds))
                {
                    timer1.Stop();
                    goto end;
                }
            }

            // TODO: ADD LANDER PHYSICS TO THIS `pbFrog`

            if (random.Next(1, 256) % 17 < 6)
            {
                PictureBox pb = new PictureBox();
                pb.Image = imgs.Images[0];
                pb.Size = new Size(32, 32);
                pb.Location = new Point(0, (int)Math.Floor(random.NextDouble() * 4 * QUARTER_Y));
                this.Controls.Add(pb);
                cars.Add(pb);
            }

            if (++counter % 10 == 0)
            {
                label1.Text = "Score: " + surviveTime++;
            }
            return;
        end:
            MessageBox.Show("You SUCK!!!");
        }
    }
}
