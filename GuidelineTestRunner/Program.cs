using System;
using AppScripting;

namespace GuidelineTestRunner
{
    public class Program
    {
        private static void Main()
        {
            var test = new Test();
            var text = test.RunTest();
            Console.WriteLine(text);
            Console.Read();
        }
    }
}