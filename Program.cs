//Copyright (c) Kirill Belozerov, 2023

using System;
using System.IO;

namespace Script
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];

            for (int i = 0; i < NUMOFCOLS; i++)
            {
                Console.WriteLine("Start column");
                ColumnProcessing(path);
                Console.WriteLine("Finish column");
            }
        }


        //Crystal Parameters:
        public static int NUMOFCOLS = 3;
        public static int DISTBETWCRYST = 100;
        public static int ALLOWEDH = 20;
        public static int ALLOWEDW = 20;
        public static int HBORDER1 = -H1 - ALLOWEDH - H2;
        public static int HBORDER2 = 1200;
        public static int WBORDER1 = 0;
        public static int WBORDER2 = 2900;
        public static int H1 = 150;
        public static int H2 = 30;
        public static int DX1 = 200;
        public static int DX2 = 250;
        public static int WIDTHOFCRYST = 2*DX1 + 3*DX2;
        //
        enum Direction
        {
            TOP = 1,
            DOWN = 2
        };


        static void LaserOn()
        {
            Console.WriteLine("Laser on");
        }
        static void LaserOff()
        {
            Console.WriteLine("Laser off");
        }
        static void GoTo(int x, int y)
        {
            Console.WriteLine($"GoTo {x} {y}");
        }
        static void FromTopToDown(int x, ref int y, bool laserNecessary)
        {
            GoTo(x, y - H2);
            y -= H2;
            if (laserNecessary == true)
                LaserOn();
            GoTo(x, y - ALLOWEDH);
            y -= ALLOWEDH;
            if (laserNecessary == true)
                LaserOff();
            GoTo(x, y - H1);
            y -= H1;
        }
        static void FromDownToTop(int x, ref int y, bool laserNecessary)
        {
            GoTo(x, y + H1);
            y += H1;
            if (laserNecessary == true)
                LaserOn();
            GoTo(x, y + ALLOWEDH);
            y += ALLOWEDH;
            if (laserNecessary == true)
                LaserOff();
            GoTo(x, y + H2);
            y += H2;
        }
        static void Autofocus()
        {
            Console.WriteLine("Autofocus 10 s +- 50 mkm z");
        }
        /*static int ReadJumpers()
        {
            string Designator = "1-2";
            if (Designator == "1-2")
                return 0;
            else if (Designator == "1-3")
                return 1;
            else if (Designator == "2-3")
                return 2;
            else if (Designator == "1")
                return 3;
            else if (Designator == "2")
                return 4;
            else if (Designator == "3")
                return 5;
            else
                return 6;
        }*/
        static void Delay(double timeInSec)
        {
            Console.WriteLine($"Delay {timeInSec} s");
        }

        //public (int, int, bool, bool, bool) CrystCoord;
        /*
        public struct CrystCoord
        {
            public int x;
            public int y;
            public bool f1;
            public bool f2;
            public bool f3;
        };*/
        static (int, int, bool, bool, bool) ReadLineFromFile(int numOfLine, string path)
        {
            int jumperNumber;
            string[] lines = File.ReadAllLines(path);
            string[] linesSplit = lines[numOfLine].Split('\t');
            string[] jumpers = linesSplit[2].Split('F');
            (int x, int y, bool f1, bool f2, bool f3) crystCoord;
            crystCoord.x = Int32.Parse(linesSplit[0]);
            crystCoord.y = Int32.Parse(linesSplit[1]);
            crystCoord.f1 = false;
            crystCoord.f2 = false;
            crystCoord.f3 = false;
            if (jumpers[0] == "-")
            {
                crystCoord.f1 = false;
                crystCoord.f2 = false;
                crystCoord.f3 = false;
            }
            else
            {
                switch (jumpers.Length)
                {
                    case 1:
                        {
                            crystCoord.f1 = false;
                            crystCoord.f2 = false;
                            crystCoord.f3 = false;
                            break;
                        }
                    case 2:
                        {
                            if (Int32.TryParse(jumpers[1], out jumperNumber))
                            {
                                switch (jumperNumber)
                                {
                                    case 1:
                                        {
                                            crystCoord.f1 = true;
                                            break;
                                        }
                                    case 2:
                                        {
                                            crystCoord.f2 = true;
                                            break;
                                        }
                                    case 3:
                                        {
                                            crystCoord.f3 = true;
                                            break;
                                        }
                                    default:
                                        {
                                            crystCoord.f1 = false;
                                            crystCoord.f2 = false;
                                            crystCoord.f3 = false;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                crystCoord.f1 = false;
                                crystCoord.f2 = false;
                                crystCoord.f3 = false;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Int32.TryParse(jumpers[1], out jumperNumber))
                            {
                                switch (jumperNumber)
                                {
                                    case 1:
                                        {
                                            crystCoord.f1 = true;
                                            break;
                                        }
                                    case 2:
                                        {
                                            crystCoord.f2 = true;
                                            break;
                                        }
                                    case 3:
                                        {
                                            crystCoord.f3 = true;
                                            break;
                                        }
                                    default:
                                        {
                                            crystCoord.f1 = false;
                                            crystCoord.f2 = false;
                                            crystCoord.f3 = false;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                crystCoord.f1 = false;
                                crystCoord.f2 = false;
                                crystCoord.f3 = false;
                            }

                            if (Int32.TryParse(jumpers[2], out jumperNumber))
                            {
                                switch (jumperNumber)
                                {
                                    case 2:
                                        {
                                            crystCoord.f2 = true;
                                            break;
                                        }
                                    case 3:
                                        {
                                            crystCoord.f3 = true;
                                            break;
                                        }
                                    default:
                                        {
                                            crystCoord.f2 = false;
                                            crystCoord.f3 = false;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                crystCoord.f2 = false;
                                crystCoord.f3 = false;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (Int32.TryParse(jumpers[1], out jumperNumber))
                            {
                                switch (jumperNumber)
                                {
                                    case 1:
                                        {
                                            crystCoord.f1 = true;
                                            break;
                                        }
                                    case 2:
                                        {
                                            crystCoord.f2 = true;
                                            break;
                                        }
                                    case 3:
                                        {
                                            crystCoord.f3 = true;
                                            break;
                                        }
                                    default:
                                        {
                                            crystCoord.f1 = false;
                                            crystCoord.f2 = false;
                                            crystCoord.f3 = false;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                crystCoord.f1 = false;
                                crystCoord.f2 = false;
                                crystCoord.f3 = false;
                            }

                            if (Int32.TryParse(jumpers[2], out jumperNumber))
                            {
                                switch (jumperNumber)
                                {
                                    case 2:
                                        {
                                            crystCoord.f2 = true;
                                            break;
                                        }
                                    case 3:
                                        {
                                            crystCoord.f3 = true;
                                            break;
                                        }
                                    default:
                                        {
                                            crystCoord.f2 = false;
                                            crystCoord.f3 = false;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                crystCoord.f2 = false;
                                crystCoord.f3 = false;
                            }

                            if (Int32.TryParse(jumpers[3], out jumperNumber))
                            {
                                switch (jumperNumber)
                                {
                                    case 3:
                                        {
                                            crystCoord.f3 = true;
                                            break;
                                        }
                                    default:
                                        {
                                            crystCoord.f3 = false;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                crystCoord.f3 = false;
                            }
                            break;
                        }
                }
            }
            return crystCoord;
        }

        static void ColumnProcessing(string path)
        {
            int x = 0, y = 0, z = 0;
            bool laserNecessary = false;
            bool firstPassage = false;
            //int typeOfJumpers;
            Direction direction = Direction.TOP;
            int countRows = 0;
            int countCryst = 0;
            int currentCryst = 0;

            (int x, int y, bool f1, bool f2, bool f3) crystCoord;

            crystCoord = ReadLineFromFile(countCryst, path);
            x = crystCoord.x;
            y = crystCoord.y;
            Delay(0.3);
            DateTime dateTime1 = DateTime.Now;
            //going to the start position
            GoTo(x, y - H1 - ALLOWEDH - H2);
            y -= H1 + ALLOWEDH + H2;
            GoTo(x + DX1, y);
            x += DX1;
            direction = Direction.TOP;
            countRows = 0;
            //typeOfJumpers = ReadJumpers();
            /*switch (typeOfJumpers)
            {
                case 0:
                    {
                        dx1 = 76;
                        dy1 = 10;
                        dx2 = 115;
                        dy2 = -10;
                        break;
                    }
                case 1:
                    {
                        dx1 = 76;
                        dy1 = 10;
                        dx2 = 230;
                        dy2 = -10;
                        break;
                    }
                case 2:
                    {
                        dx1 = 115;
                        dy1 = 10;
                        dx2 = 115;
                        dy2 = -10;
                        break;
                    }
                case 3:
                    {
                        dx1 = 76;
                        dy1 = 0;
                        dx2 = 0;
                        dy2 = 0;
                        break;
                    }
                case 4:
                    {
                        dx1 = 0;
                        dy1 = 0;
                        dx2 = 115;
                        dy2 = 0;
                        break;
                    }
                case 5:
                    {
                        dx1 = 0;
                        dy1 = 0;
                        dx2 = 115;
                        dy2 = 0;
                        break;
                    }
                case 6:
                    {
                        dx1 = 76;
                        dy1 = 0;
                        dx2 = 0;
                        dy2 = 0;
                        break;
                    }
                default:
                    break;
            }*/

            firstPassage = true;
            while (countRows != 3)
            {
                switch (direction)
                {
                    case Direction.TOP:
                        {
                            while (y != HBORDER2)
                            {
                                if (firstPassage)
                                    countCryst++;
                                //currentCryst++;
                                crystCoord = ReadLineFromFile(currentCryst, path);
                                currentCryst++;
                                switch (countRows)
                                {
                                    case 0:
                                        {
                                            if (crystCoord.f1)
                                                laserNecessary = true;
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (crystCoord.f2)
                                                laserNecessary = true;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (crystCoord.f3)
                                                laserNecessary = true;
                                            break;
                                        }
                                }
                                FromDownToTop(x, ref y, laserNecessary);
                                laserNecessary = false;
                                //y += H1 + ALLOWEDH + H2;
                                if (y != HBORDER2)
                                {
                                    GoTo(x, y + DISTBETWCRYST);
                                    y += DISTBETWCRYST;
                                }
                                else
                                {
                                    countRows++;
                                    if (countRows == 1)
                                        firstPassage = false;
                                    if (countRows != 3)
                                    {
                                        GoTo(x + DX2, y);
                                        x += DX2;
                                    }
                                    direction = Direction.DOWN;
                                    break;
                                }
                            }
                            break;
                        }
                    case Direction.DOWN:
                        {
                            while (y != HBORDER1)
                            {
                                currentCryst--;
                                crystCoord = ReadLineFromFile(currentCryst, path);
                                //x = crystCoord.x;
                                //y = crystCoord.y;
                                switch (countRows)
                                {
                                    case 0:
                                        {
                                            if (crystCoord.f1)
                                                laserNecessary = true;
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (crystCoord.f2)
                                                laserNecessary = true;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (crystCoord.f3)
                                                laserNecessary = true;
                                            break;
                                        }
                                }
                                FromTopToDown(x, ref y, laserNecessary);
                                //y -= H1 + ALLOWEDH + H2;
                                laserNecessary = false;
                                if (y != HBORDER1)
                                {
                                    GoTo(x, y - DISTBETWCRYST);
                                    y -= DISTBETWCRYST;
                                }
                                else
                                {
                                    countRows++;
                                    GoTo(x + DX2, y);
                                    x += DX2;
                                    direction = Direction.TOP;
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
            DateTime dateTime2 = DateTime.Now;
            //Console.WriteLine(dateTime2);
            if (dateTime2.Subtract(dateTime1).TotalSeconds > 10)
                Autofocus();
        }

    }
}
