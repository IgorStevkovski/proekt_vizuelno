using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_ver1
{
    [Serializable]
    public class Dokument
    {
        public List<Ball> balls;
        public List<Strela> streli;
        public Strelec strelec;
        public Rectangle frame;
        public float brzinaStrela { get; set; }
        public int brojStreli { get; set; }
        
        public int vkupnoPoeni;
        public int nivo;
        public Dokument(Rectangle frame)
        {
            balls = new List<Ball>();
            streli = new List<Strela>();
            strelec = new Strelec();
            vkupnoPoeni = 0;
            this.frame = frame;
            brzinaStrela = 10;
            brojStreli = 7;
            nivo = 1;

            osnovajTopcinja();
        }

        public void osnovajTopcinja()
        {
            Ball ball;
            Point teme;
            Random random = new Random();

            for (int i = 0; i <= 13; i++)
            {
                int y = frame.Height;
                int x = 600 + balls.Count * (Ball.RADIUS * 2 + 20);
                teme = new Point(x, y);

                int broj = random.Next(6);// za bojata a so toa i brzinata i poenite
                if (broj == 0)
                {
                    ball = new Ball();
                    ball.teme = teme;
                    ball.color = Color.DarkGoldenrod;
                    ball.velocityY = -random.Next(5, 15);
                    ball.poeni = 3;
                    ball.pogodeno = false;

                    balls.Add(ball);
                }
                else if (broj == 1 || broj == 2)
                {
                    ball = new Ball();
                    ball.teme = teme;
                    ball.color = Color.ForestGreen;
                    ball.velocityY = -random.Next(5, 15);
                    ball.poeni = 2;
                    ball.pogodeno = false;

                    balls.Add(ball);
                }
                else
                {
                    ball = new Ball();
                    ball.teme = teme;
                    ball.color = Color.DarkViolet;
                    ball.velocityY = -random.Next(5, 15);
                    ball.poeni = 1;
                    ball.pogodeno = false;

                    balls.Add(ball);
                }
            }
        }
        public void draw(Graphics g)
        {
            strelec.draw(g);

            foreach (Ball b in balls)
            {
                b.draw(g);
            }

            foreach(Strela s in streli)
            {
                s.draw(g);
            }
        }

        public void checkCollisions()
        {
            for (int i = 0; i < streli.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (balls[j].isHit(streli[i].vtora))
                    {
                        vkupnoPoeni += balls[j].poeni;
                        balls.RemoveAt(j);
                    }
                }
            }
        }

        public void move()
        {
            foreach (Ball b in balls)
            {
                b.move(frame);
            }
            for (int i = 0; i < streli.Count; i++)
            {
                if (streli[i].izlegla == false)
                    streli[i].move(frame);
                else
                    streli.RemoveAt(i);
            }
        }

    }
}
