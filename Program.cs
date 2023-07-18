using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Script
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0, dx1 = 0, dx2 = 0, dy1 = 0, dy2 = 0, z = 0, j = 0;
            bool endOfCol1 = false, endOfCol2 = false;
            //int typeOfJumpers;
            int numOfNormCryst = 4;
            const int NUMOFCOLS = 10;
            const int WIDTHOFCRYST = 900;
            const int DISTBETWCRYST = 100;
            const int ALLOWEDH = 20;
            const int ALLOWEDW = 20;
            const int H1 = 150;
            const int H2 = 50;
            const int HBORDER1 = 0;
            const int HBORDER2 = 4000;
            const int WBORDER1 = 0;
            const int WBORDER2 = 4000;

            CrystCoord crystCoord;

            for (int i = 0; i < NUMOFCOLS; i++)
            {

                Delay(0.3);
                DateTime dateTime = DateTime.Now;
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
                while (!endOfCol1||!endOfCol2)
                {
                    crystCoord = ReadLineFromFile(j);
                    if (crystCoord.y == HBORDER1)
                        endOfCol1 = true;
                    if (crystCoord.y == HBORDER2)
                        endOfCol2 = true;


                }
                GoTo(x + dx1, y);
                LaserOn();
                GoTo(x, y + dy1);
                LaserOff();
                GoTo(x + dx2, y);
                LaserOn();
                GoTo(x, y + dy2);
                Console.WriteLine(dateTime);
            }
        }
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
            Console.WriteLine($"Goto {x} {y}");
        }
        static void Autofocus()
        {
            Console.WriteLine("Autofocus");
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
            Console.WriteLine($"Delay {timeInSec} c");
        }
        public struct CrystCoord
        {
            public CrystCoord(int x = 0, int y = 0, bool f1 = false, bool f2 = false, bool f3 = false)
            {
                this.x = x;
                this.y = y;
                this.f1 = f1;
                this.f2 = f2;
                this.f3 = f3;
            }

            public int x;
            public int y;
            public bool f1;
            public bool f2;
            public bool f3;
        };
        static CrystCoord ReadLineFromFile(int numOfLine)
        {
            string[] lines = File.ReadAllLines("Crystal Coordinates.txt");
            string[] linesSplit = lines[numOfLine].Split('\t');
            string[] jumpers = linesSplit[2].Split('F');
            CrystCoord crystCoord;
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
                try
                {
                    switch (Int32.Parse(jumpers[1]))
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
                catch (Exception)
                {
                    crystCoord.f1 = false;
                }

                try
                {
                    switch (Int32.Parse(jumpers[2]))
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
                catch (Exception)
                {
                    crystCoord.f2 = false;
                }

                try
                {
                    if (Int32.Parse(jumpers[2]) == 3)
                        crystCoord.f3 = true;
                    else
                        crystCoord.f3 = false;
                }
                catch (Exception)
                {
                    crystCoord.f3 = false;
                }
            }
            
            return crystCoord;
        }
    }
}