using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
using static System.Math;

namespace AtCoder
{
    public class ABC135
    {
        public void SolveA(ConsoleInput cin)
        {
            int a = cin.ReadInt;
            int b = cin.ReadInt;
            if ((a + b) % 2 == 0)
            {
                WriteLine((a+b)/2);
            }
            else
            {
                WriteLine("IMPOSSIBLE");
            }
            ReadLine();
        }
    }
}
