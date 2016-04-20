using System;
using System.Collections.Generic;
using System.Threading;

namespace Tanks
{
    public delegate void ChangeGameStatusDelegate();
    class Model
    {
        public event ChangeGameStatusDelegate changeStreep;
        public GameStatus gameStatus;


        int collectedApples;
        int sizeField;
        int amountTanks;
        int amountApples;
        public int speedGame;
        int step; //шаг для рисования подобранных яблок на определенном месте

        
        Random r;
        Bullet bullet;
        MyTank myTank;
        List<Tank> tanks;
        List<Apple> apples;
        Wall wall;

        internal Bullet Bullet
        {
            get { return bullet; }
        }
        internal MyTank MyTank
        {
            get { return myTank; }
        }
        internal List<Tank> Tanks
        {
            get { return tanks; }
        }
        internal List<Apple> Apples
        {
            get { return apples; }
        }
        internal Wall Wall
        {
            get { return wall; }
        }

        public Model(int sizeField, int amountTanks, int amountApples, int speedGame)
        {
            r = new Random();

            this.sizeField = sizeField;
            this.amountTanks = amountTanks;
            this.amountApples = amountApples;
            this.speedGame = speedGame;

            NewGame();
        }
        public void NewGame()
        {
            collectedApples = 0;
            step = -25;
            bullet = new Bullet();
            myTank = new MyTank(sizeField);
            tanks = new List<Tank>();
            apples = new List<Apple>();

            CreateTanks();
            CreateApples();
            wall = new Wall();

            gameStatus = GameStatus.stoping;
        }
        public void Play()
        {
            while (gameStatus == GameStatus.playing)
            {
                Thread.Sleep(speedGame);

                bullet.Run();
                myTank.Run();
                foreach (Tank t in tanks)
                    t.Run();

                TryDestroyTank();
                IfCollisionOfTanks();
                IfCollisionOfTankAndPacman();
                PickAppleByMyTank();

                if (collectedApples > 4)
                {
                    gameStatus = GameStatus.winer;
                    if (changeStreep != null)
                        changeStreep();
                }
            }
        }


        private void PickAppleByMyTank()
        {
            //подбор яблок танком игрока
            for (int i = 0; i < apples.Count; i++)
            {
                if (Math.Abs(myTank.X - apples[i].X) < 4 && Math.Abs(myTank.Y - apples[i].Y) < 4)
                {
                    apples[i] = new Apple(step += 25, 265);
                    CreateApples(++collectedApples);
                }
            }
        }
        private void IfCollisionOfTankAndPacman()
        {
            //столкновение вражеских танков c танком игрока
            for (int i = 0; i < tanks.Count; i++)
            {
                if (
                            (Math.Abs(tanks[i].X - myTank.X) <= 19 && (tanks[i].Y == myTank.Y))
                        ||
                            (Math.Abs(tanks[i].Y - myTank.Y) <= 19 && (tanks[i].X == myTank.X))
                        ||
                            (Math.Abs(tanks[i].X - myTank.X) <= 19 && Math.Abs(tanks[i].Y - myTank.Y) <= 19)
                   )
                {
                    gameStatus = GameStatus.loozer;
                    if (changeStreep != null)
                        changeStreep();
                }
            }
        }
        private void IfCollisionOfTanks()
        {
            // столкновение вражеских танков между собой
            for (int i = 0; i < tanks.Count - 1; i++)
                for (int j = i + 1; j < tanks.Count; j++)
                    if (
                            (Math.Abs(tanks[i].X - tanks[j].X) <= 20 && (tanks[i].Y == tanks[j].Y))
                        ||
                            (Math.Abs(tanks[i].Y - tanks[j].Y) <= 20 && (tanks[i].X == tanks[j].X))
                        ||
                            (Math.Abs(tanks[i].X - tanks[j].X) <= 20 && Math.Abs(tanks[i].Y - tanks[j].Y) <= 20)
                        )
                    {
                        tanks[i].TurnAround();
                        tanks[j].TurnAround();
                    }
        }
        private void TryDestroyTank()
        {
            //поражение снарядом вражеских танков
            for (int i = 0; i < tanks.Count; i++)
            {
                if ((bullet.X - tanks[i].X) < 14 && (bullet.Y - tanks[i].Y) < 14 &&
                     (bullet.X - tanks[i].X) > 4 && (bullet.Y - tanks[i].Y) > 4)
                {
                    tanks.RemoveAt(i);
                    bullet.DefaultSettings();
                }
            }
        }
        private void CreateApples()
        {
            CreateApples(0);
        }
        private void CreateApples(int newApples)
        {
            int x, y;
            while (apples.Count < amountApples + newApples)
            {
                x = r.Next(6) * 40;
                y = r.Next(6) * 40;
                bool flag = true;

                foreach (Apple a in apples)
                    if (a.X == x && a.Y == y)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    apples.Add(new Apple(x, y));
            }
        }
        private void CreateTanks()
        {
            int x, y;
            while (tanks.Count < amountTanks)
            {
                x = r.Next(6) * 40;
                y = r.Next(6) * 40;
                bool flag = true;

                foreach (Tank t in tanks)
                    if (t.X == x && t.Y == y)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    tanks.Add(new Tank(sizeField, x, y));
            }
        }
    }
}
