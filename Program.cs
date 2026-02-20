using System;

namespace NewCSharp
{
  class Program
  {
    static void Main(string[] args)
    {
      for (int i = 0; i < 10; i++)
      {
        if (i == 4)
        {
          Console.WriteLine("it reached the continue statement");
          continue;

        }
        Console.WriteLine(i);
      }
    }
  }
}