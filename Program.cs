using System;

namespace NewCSharp
{
  class Program
  {
    static void Main(string[] args)
    {
      // Full name
      string name = "John Doe";

      // Location of the letter D
      int charPos = name.IndexOf("D");

      // Get last name
      string lastName = name.Substring(charPos);

      // Print the result
      Console.WriteLine(lastName);
    }
  }
}