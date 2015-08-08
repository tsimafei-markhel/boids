using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Boids
{
    public partial class Form1 : Form
    {
        private Flock flock = null;
        private FlockController flockController;
        private Random random = new Random();

        private AvoidBordersRule avoidBordersRule;

        public Form1()
        {
            InitializeComponent();

            flockController = new FlockController();
            flockController.MoveRules.Add(new CenterOfMassRule(0.1f));
            flockController.MoveRules.Add(new KeepDistanceBetweenBoidsRule(10.0f));
            flockController.MoveRules.Add(new MatchVelocityRule(0.1f));

            avoidBordersRule = new AvoidBordersRule(this.pictureBox1.ClientRectangle, 50.0f);
            flockController.MoveRules.Add(avoidBordersRule);

            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (flock != null)
            {
                flockController.Move(flock);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                flock = CreateFlock(e.Location);
                pictureBox1.Invalidate();
                timer1.Start();
            }
            else if (e.Button == MouseButtons.Right)
            {
                timer1.Stop();
                RemoveFlock();
            }
        }

        private Flock CreateFlock(Point targetLocation)
        {
            Flock newFlock = new Flock();

            int minBoids = 2;// 1;
            int maxBoids = 20;// 2;
            float positionDelta = 50;
            int velocityDelta = 10;

            int numberOfBoids = random.Next(minBoids, maxBoids);
            float minX = targetLocation.X - positionDelta;
            float maxX = targetLocation.X + positionDelta;
            float minY = targetLocation.Y - positionDelta;
            float maxY = targetLocation.Y + positionDelta;

            float deltaX = Math.Abs(maxX - minX);
            float deltaY = Math.Abs(maxY - minY);

            for (int i = 0; i < numberOfBoids; i++)
            {
                Boid boid = new Boid
                {
                    Velocity = new Vector2((float)random.Next(-velocityDelta, velocityDelta), (float)random.Next(-velocityDelta, velocityDelta)),
                    Position = new Vector2(minX + (float)random.NextDouble() * deltaX, minY + (float)random.NextDouble() * deltaY)
                };

                newFlock.Boids.Add(boid);
            }

            return newFlock;
        }

        private void RemoveFlock()
        {
            flock = null;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.SuspendLayout();

            e.Graphics.Clear(pictureBox1.BackColor);

            if (flock != null)
            {
                foreach (Boid boid in flock.Boids)
                {
                    e.Graphics.FillEllipse(Brushes.Green, boid.Position.X - 2.5f, boid.Position.Y - 2.5f, 5f, 5f);
                }
            }

            pictureBox1.ResumeLayout();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            this.avoidBordersRule.AllowedArea = this.pictureBox1.ClientRectangle;
        }
    }
}