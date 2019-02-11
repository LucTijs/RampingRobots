using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RampingRobots
{
    class Factory
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Robots { get; set; }
        public int Turn { get; set; }
        public bool RobotsMove { get; set; }
        public string Move { get; set; }
        public Robot Rob { get; set; }
        private bool SameSpot { get; set; }
        public Factory(int width, int height, int robots, int turn, bool robotsMove)
        {
            this.Width = width;
            this.Height = height;
            this.Robots = robots;
            this.Turn = turn;
            this.RobotsMove = robotsMove;
        }

        public void Run()
        {
            List<Robot> RobotList = new List<Robot>();
            for (int i = 0; i < this.Robots; i++)
            {
                RobotList.Add(new Robot(this.Height,this.Width)); //Creer list voor alle Robots
            }

            foreach (var Robot in RobotList)
            {
                this.SameSpot = true;
                while (this.SameSpot == true)
                {
                    this.SameSpot = false; //Op false zetten zodat hij kan veranderen als waardes gelijk zijn
                    Robot.GetRow("Random");
                    foreach (var Robot2 in RobotList)
                    {
                        if (Robot != Robot2)
                        {
                            if (Robot.Col == Robot2.Col && Robot.Row == Robot2.Row)
                            {
                                this.SameSpot = true; //Zorgen dat we stap herhalen
                                Robot.Row = Robot.pRow;
                                Robot.Col = Robot.pCol;
                            }
                        }
                    }
                }
            }           
            Mechanic M = new Mechanic(this.Height,this.Width); //Creer Mechanic

            for (int i = 0; i < this.Turn; i++) //Beurten
            {
                Console.Out.WriteLine("Turns left:", this.Turn);
                this.Turn = this.Turn - 1;
                Console.Out.WriteLine("Enter your moves with WASD");
                this.Move = Console.ReadLine();
                M.GetCol(this.Move); //Mechanic Laten bewegen
                M.GetRow(this.Move);
                if (RobotsMove)
                {
                    foreach (var Robot in RobotList)
                    {
                        this.SameSpot = true; //Op true zetten zodat hij whileloop in gaat
                        while (this.SameSpot == true)
                        {
                            this.SameSpot = false; //Op false zetten zodat hij kan veranderen als waardes gelijk zijn
                            Robot.GetRow(this.Move);
                            foreach (var Robot2 in RobotList)
                            {


                                if (Robot != Robot2)
                                {
                                    if (Robot.Col == Robot2.Col && Robot.Row == Robot2.Row)
                                    {
                                        this.SameSpot = true; //Zorgen dat we stap herhalen
                                        Robot.Row = Robot.pRow;
                                        Robot.Col = Robot.pCol;
                                    }
                                }
                            }
                        }

                    }
                }
                    
                       
                    
                
                

                RobotList.RemoveAll(Robot => (Robot.Col == M.Col) && (Robot.Row == M.Row));
                
                string StandardLine = "";
                string StandardLineLoop;
                var aStringBuilder = new StringBuilder(StandardLine);
                for (int j = 0; j < this.Width; j++)
                {
                    aStringBuilder.Insert(j, "."); //Hier wordt de breedte gemaakt van de fabriek
                }
                StandardLine = aStringBuilder.ToString();
                for (int j = 0; j < this.Height; j++)//Loopen Rows
                {
                    var StringbuilderLoop = new StringBuilder(StandardLine);
                    if (j == M.Row - 1)
                    {

                        StringbuilderLoop.Remove(M.Col-1, 1);
                        StringbuilderLoop.Insert(M.Col-1, "M");

                    }

                    foreach (var Robot in RobotList)
                    {
                        if (Robot.Row-1 == j)
                        {
                            StringbuilderLoop.Remove(Robot.Col-1, 1);
                            StringbuilderLoop.Insert(Robot.Col-1, "R");
                        }
                    }
                    StandardLineLoop = StringbuilderLoop.ToString();
                    Console.Out.WriteLine(StandardLineLoop);

                }



            }






        }

    }
    class Randomnummer
    {
        public int Min {get;set;}
        public int Max { get; set; }

        public Randomnummer(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }
        public int Randomnr()
        {
            Random random = new Random();
            return random.Next(this.Min, this.Max);
        }
    }
    class Mechanic
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int MRow { get; set; }
        public int MCol { get; set; }
        public Mechanic(int mrow, int mcol)
        {
            this.Row = 1;
            this.Col = 1;
            this.MRow = mrow;
            this.MCol = mcol;
        }


        public double GetRow(string Movements)
        {
            foreach (char i in Movements)
            {
                if (i.ToString() == "w")
                {
                    this.Row = this.Row - 1;
                }

                if (i.ToString() == "s")
                {
                    this.Row = this.Row + 1;
                }

                if (this.Row == 0)
                {
                    this.Row = 1;
                }

                if (this.Row == this.MRow+1)
                {
                    this.Row = this.MRow;
                }
            }

            return this.Row;
        }

        public double GetCol(string Movements)
        {
            foreach (char i in Movements)
            {

                if (i.ToString() == "a")
                {
                    this.Col = this.Col - 1;
                }

                if (i.ToString() == "d")
                {
                    this.Col = this.Col + 1;
                }

                if (this.Col == 0)
                {
                    this.Col = 1;
                }

                if (this.Col == this.MCol+1)
                {
                    this.Col = this.MCol;
                }
            }

            return this.Col;
        }

    }

    class Robot
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Name { get; set; }
        public int pRow { get; set; }
        public int pCol { get; set; }
        public int MRow { get; set; }
        public int MCol { get; set; }
        public Robot(int mrow,int mcol)
        {
            Randomnummer row = new Randomnummer(1, mrow); //Random row genereren 4 rijen
            Randomnummer col = new Randomnummer(1, mcol);//random Col generen
            this.Row = row.Randomnr();
            this.Col = col.Randomnr();
            this.MRow = mrow;
            this.MCol = mcol;
        }


        public double GetRow(string Movements)
        {
            foreach (char i in Movements)
            {
                this.pCol = Col; //Voor Controle
                this.pRow = Row; //Voor controle
                Randomnummer N = new Randomnummer(1, 4); //binnen de loop om iedere keer een nieuwe 'move
                int move = N.Randomnr();
                if (move == 1)
                {
                    this.Row = this.Row - 1;
                }

                if (move == 2)
                {
                    this.Row = this.Row + 1;
                }

                if (this.Row == 0)
                {
                    this.Row = 1;
                }

                if (this.Row == this.MRow+1)
                {
                    this.Row = this.MRow;
                }

                if (move == 3)
                {
                    this.Col = this.Col - 1;
                }

                if (move == 4)
                {
                    this.Col = this.Col + 1;
                }

                if (this.Col == 0)
                {
                    this.Col = 1;
                }

                if (this.Col == this.MCol+1)
                {
                    this.Col = this.MCol;
                }
            }

            return this.Row; //moet een waarde terug gegeven worden
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.Out.WriteLine("Hoe Breed Moet de Fabriek zijn? (aantal Kolommen)");
                int Rijen = Convert.ToInt32(Console.ReadLine());
                Console.Out.WriteLine("Hoe Hoog moet de Fabriek zijn? (aantal rijen)");
                int Kolom = Convert.ToInt32(Console.ReadLine());
                Console.Out.WriteLine("Hoeveel Robots wil je ?");
                int robots = Convert.ToInt32(Console.ReadLine());
                Console.Out.WriteLine("Hoeveel beurten wil je?");
                int turns = Convert.ToInt32(Console.ReadLine());
                Console.Out.WriteLine("Wil je dat de robots bewegen?(j of n)");
                string RMove = Console.ReadLine();
                bool RobotsMove = (RMove == "j");
                Factory F=new Factory(Rijen,Kolom,robots,turns, RobotsMove);
                F.Run();
                Console.ReadLine();
            }
        }




    }
}
