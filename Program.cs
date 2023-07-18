using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Script
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0, dx1, dx2, dy1, dy2, z, time;
            int typeOfJumpers;
            const int N = 10;

            for (int i = 0; i < N; i++)
            {

                DateTime dateTime = new DateTime();
                Console.WriteLine(dateTime);
                typeOfJumpers = ReadJumpers();
                switch (typeOfJumpers)
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
                }
                GoTo(x + dx1, y);
                LaserOn();
                GoTo(x, y + dy1);
                LaserOff();
                GoTo(x + dx2, y);
                LaserOn();
                GoTo(x, y + dy2);
            }
        }
        static void LaserOn()
        {

        }
        static void LaserOff()
        {

        }
        static void GoTo(int x, int y) 
        { 

        }
        static void Autofocus()
        {
            
        }
        static int ReadJumpers()
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
        }
        static struct CrystCoord
        {
            public CrystCoord(int x = 0, int y = 0) 
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        };
    }
}
