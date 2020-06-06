using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace deneme
{
    public class deneme
    {

        static List<int> method1(int n)
        {
            List<int> primes = new List<int>() { 2 };
            int counter = 3;
            while (primes.Count < n)
            {
                bool isPrime = true;
                for (int i = 3; i <= Math.Sqrt(counter); i += 2)
                {
                    if (counter % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(counter);
                }
                counter += 2;
            }
            return primes;
        }

        static List<int> method2(int n)
        {
            List<int> primes = new List<int>() { 2 };
            int counter = 3;
            while (primes.Count < n)
            {
                bool isPrime = true;
                for (int i = 0; i < primes.Count; i++)
                {
                    int check = primes[i];
                    if (check > Math.Sqrt(counter))
                    {
                        break;
                    }
                    else
                    {
                        if (counter % check == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                }
                if (isPrime)
                {
                    primes.Add(counter);
                }
                counter += 2;
            }
            return primes;
        }

        static List<int> SieveOfAtkin(int limit)
        {
            List<int> primes = new List<int>();
            // 2 and 3 are known to be prime 
            if (limit > 2)
                primes.Add(2);

            if (limit > 3)
               primes.Add(3);

            // Initialise the sieve array with 
            // false values 
            bool[] sieve = new bool[limit];

            for (int i = 0; i < limit; i++)
                sieve[i] = false;

            /* Mark siev[n] is true if one of the 
            following is true: 
            a) n = (4*x*x)+(y*y) has odd number  
               of solutions, i.e., there exist  
               odd number of distinct pairs  
               (x, y) that satisfy the equation  
               and    n % 12 = 1 or n % 12 = 5. 
            b) n = (3*x*x)+(y*y) has odd number  
               of solutions and n % 12 = 7 
            c) n = (3*x*x)-(y*y) has odd number  
               of solutions, x > y and n % 12 = 11 */
            for (int x = 1; x * x < limit; x++)
            {
                for (int y = 1; y * y < limit; y++)
                {

                    // Main part of Sieve of Atkin 
                    int n = (4 * x * x) + (y * y);
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))

                        sieve[n] ^= true;

                    n = (3 * x * x) + (y * y);
                    if (n <= limit && n % 12 == 7)
                        sieve[n] ^= true;

                    n = (3 * x * x) - (y * y);
                    if (x > y && n <= limit && n % 12 == 11)
                        sieve[n] ^= true;
                }
            }

            // Mark all multiples of squares as 
            // non-prime 
            for (int r = 5; r * r < limit; r++)
            {
                if (sieve[r])
                {
                    for (int i = r * r; i < limit;
                         i += r * r)
                        sieve[i] = false;
                }
            }

            // Print primes using sieve[] 
            for (int a = 5; a < limit; a++)
                if (sieve[a])
                    primes.Add(a);
            return primes;
        }

        static void Main()
        {
            int n = 10000000; // 10 milyon
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<int> primes2 = method2(n);
            stopwatch.Stop();
            Console.WriteLine("method2: " + stopwatch.Elapsed);
            stopwatch.Restart();
            List<int> primes1 = method1(n);
            stopwatch.Stop();
            Console.WriteLine("method1: " + stopwatch.Elapsed);
            int limit = 1000000000; // 1 milyar
            stopwatch.Restart();
            List<int> soa = SieveOfAtkin(limit);
            stopwatch.Stop();
            Console.WriteLine(soa.Count + " primes found");
            Console.WriteLine("Sieve of Atkin: " + stopwatch.Elapsed);
        }
    }
}
