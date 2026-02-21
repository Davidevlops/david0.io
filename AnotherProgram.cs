using System;

namespace New2DLoop
{
    public class AnotherProgram
    {
        public static void AnotherMain()
        {
            int[,] numbers = { { 1, 4, 2 }, { 3, 6, 8 }, { 5, 7, 9 } };

            Console.WriteLine(numbers[2, 2]);
        }
    }
}