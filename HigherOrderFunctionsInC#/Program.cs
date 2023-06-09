using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HelloWorld
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string text = "abcdefghijklnmñopqrstvwxyz";

            Console.WriteLine("Method One");
            TimeLogger("While and For",() => MethodOne(text));
            Console.WriteLine("Method Two");
            TimeLogger("Nested For",() => MethodTwo(text));
            Console.WriteLine("Method Three");
            TimeLogger("Recursion",() => MethodThree(text));

            Console.ReadKey();
        }

        private static void MethodOne(string text)
        {
            int textLength = text.Length;
            bool lastPosition = false;
            int currentPosition = 0;

            while (!lastPosition)
            {
                for (int i = 1; i <= (textLength - currentPosition); i++)
                {
                    Console.WriteLine(text.Substring(currentPosition, i));
                }

                currentPosition++;
                if (currentPosition == textLength)
                {
                    lastPosition = true;
                }
            }
        }

        private static void MethodTwo(string text)
        {
            int textLength = text.Length;
            for (int i = 0; i < textLength; i++)
            {
                for (int j = 1; j <= (textLength - i); j++)
                {
                    Console.WriteLine(text.Substring(i, j));
                }
            }
        }

        private static void MethodThree(string text, int currentPosition = 0, int charactersLength = 1)
        {
            int textLength = text.Length;
            if (currentPosition == textLength)
            {
                return;
            }

            Console.WriteLine(text.Substring(currentPosition, charactersLength));

            if (charactersLength == (textLength - currentPosition))
            {
                currentPosition++;
                charactersLength = 1;
            }
            else 
            {
                charactersLength++;
            }

            MethodThree(text, currentPosition, charactersLength);
        }

        private static void TimeLogger(string caller, Action action) 
        {
            DateTime startTime = DateTime.Now;

            action.Invoke();

            DateTime endTime = DateTime.Now;
            double totalTime = endTime.Subtract(startTime).TotalSeconds;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total operation time for {caller} method: {totalTime}");
            Console.ResetColor();
        }
	}
}
