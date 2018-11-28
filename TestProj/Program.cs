using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guessing
{
    class ShannonGuessing
    {        
        static void Main(string[] args)
        {
            char chr;
            Guesser g = new Guesser();

            //читаем данные - то число которое загадал человек
            do
            {
                Console.WriteLine("Write only 0 or 1;\t\tPress 5 to exit");

                chr = Console.ReadKey().KeyChar;
                Console.WriteLine("");

                if (chr == '5')
                {
                    Environment.Exit(0);
                }
                else if (chr == '1' || chr == '0')
                {
                    var humanNumChoose = (int)Char.GetNumericValue(chr);
                    
                    if (g.IsWinnerIsHuman(humanNumChoose))
                    {
                        Console.Write("You WIN! \t");
                    }
                    else
                    {
                        Console.Write("LOOSER!; \t");
                    }

                    Console.WriteLine($"Human:{g.ScoreHuman} : CPU:{g.ScoreCpu};\t % of CPU wins:{g.ScoreCpuInPercents}");

                    g.CalcNextPrediction();
                }
            } while (true);
        }
    }


}


