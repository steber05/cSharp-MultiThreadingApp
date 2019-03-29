using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreadingApp
{
    class Program
    {
        //global static variables
        private static Object thisLock = new Object();
        public static long max;
        public static long[] a;

        public static void Main(String[] args)
        {
            max = 0;
            string file;// = "C:\\Users\\stevi\\Desktop\\ints.txt";
            //Read file to array
            System.Console.WriteLine("Enter a file path: ");
            file = Console.ReadLine();
            string[] arr = System.IO.File.ReadAllLines(file);
            a = new long[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                a[i] = long.Parse(arr[i]);
            }
            //Threading
            ThreadStart threadStart1 = new ThreadStart(FindMaxOne);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();
            ThreadStart threadStart2 = new ThreadStart(FindMaxTwo);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();
            //setup sleep times. get wrong outputs everytime i ran with no sleep timers
            while (thread1.IsAlive || thread2.IsAlive)
            {
                Thread.Sleep(5);
            }
            //Output maximum to console
            System.Console.WriteLine("The maximum number in the array is: {0}", max);
            Console.ReadLine();
            return;
        }
        //removed getting parameters to the methods because threads were not working
        //i was getting errors in my ThreadStarts
        //Made the array a static global variable to work around this
        public static void FindMaxOne()
        {
            for (int i = 0; i < a.Length / 2; i++) lock (thisLock)
                {
                    if (a[i] > max)
                    {
                        max = a[i];
                    }
                }
        }

        public static void FindMaxTwo()
        {
            for (int j = a.Length / 2 + 1; j < a.Length; j++) lock (thisLock)
                {
                    if (a[j] > max)
                    {
                        max = a[j];
                    }
                }
        }
    }
}
