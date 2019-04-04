using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

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
            //Variables
            Stopwatch aTimer = new Stopwatch();
            Stopwatch bTimer = new Stopwatch();
            string file;


            //get user input for file and intialize array
            System.Console.WriteLine("Enter a file path: ");
            file = Console.ReadLine();
            Console.Clear();
            string[] arr = System.IO.File.ReadAllLines(file);
            a = new long[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                a[i] = long.Parse(arr[i]);
            }

            //multithreading
            //set first timer to start
            aTimer.Start();
            max = 0;
            Console.WriteLine("\t\tMultithreading");
            //Threading
            ThreadStart threadStart1 = new ThreadStart(FindMaxOne);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();
            ThreadStart threadStart2 = new ThreadStart(FindMaxTwo);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();
            //setup sleep times for threads. get wrong outputs everytime i ran with no sleep timers
            while (thread1.IsAlive || thread2.IsAlive)
            {
                Thread.Sleep(5);
            }
            //output maximum to console
            System.Console.WriteLine("The maximum number in the array is: {0}", max);
            //stop first timer and output elapsed time
            aTimer.Stop();
            Console.WriteLine("Multithreaded proccess time: {0}", aTimer.Elapsed);


            //Single Thread
            //start second timer
            bTimer.Start();
            max = 0;
            Console.WriteLine("\n\n\n\t\t Single Thread");
            SingleThread();
            //output maximum to console
            System.Console.WriteLine("The maximum number in the array is: {0}", max);
            //stop second timer and output elapsed time
            bTimer.Stop();
            Console.WriteLine("Singlethreaded proccess time: {0}", bTimer.Elapsed);
            

            //prompt end of program
            Console.WriteLine("\n\nPress enter to exit...");
            Console.ReadLine();
            return;
        }
        //removed getting parameters to the methods because threads were not working
        //i was getting errors in my ThreadStarts
        //Made the array a static global variable to work around this
        public static void FindMaxOne()
        {
            for (int i = 0; i <= a.Length / 2; i++) lock (thisLock)
                {
                    if (a[i] > max)
                    {
                        max = a[i];
                    }
                }
        }//end of FindMaxOne

        public static void FindMaxTwo()
        {
            for (int j = a.Length / 2 + 1; j < a.Length; j++) lock (thisLock)
                {
                    if (a[j] > max)
                    {
                        max = a[j];
                    }
                }
        }//end of FindMaxTwo

        public static void SingleThread()
        {
            for (int k=0;k<a.Length;k++)
            {
                if(a[k] > max)
                {
                    max = a[k];
                }
            }
        }//end of SingleThread
    }
}
