using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secret
{
    class Program
    {
        static int f (int n, int x, int[] coef,int M)
        {
            int sum = M;
            for(int i=1;i<=n;i++)
            {
                sum += (int)Math.Pow(x, i) * coef[i - 1];
            }
            return sum;
        }

        static double li(int i, int k, double[,] keys)
        {
            double pr = 1;
            for(int j=0;j<k;j++)
            {
                if(j!=i)
                    pr *= (0-keys[j,0]) / (keys[i,0]-keys[j,0]);
                //Console.WriteLine(keys[i, 0] + " " + keys[j, 0]);
            }
            //Console.WriteLine(pr);
            return pr;
        }

        static void Main(string[] args)
        {
            //share secret
            string str;
            int M, n, k;
            Console.WriteLine("Enter secret");
            str = Console.ReadLine();
            Int32.TryParse(str, out M);
            Console.WriteLine("Enter number of friends");
            str = Console.ReadLine();
            Int32.TryParse(str, out n);
            Console.WriteLine("Enter number needed to retrieve secret");
            str = Console.ReadLine();
            Int32.TryParse(str, out k);
            int[] coef = new int[k - 1];
            Console.Write("function is f(x)="+M);
            Random rnd = new Random();
            for (int i=0;i<k-1;i++)
            {
                int nxt = rnd.Next(100);
                if (nxt != 0)
                    coef[i] = nxt;
                Console.Write("+" + coef[i] + "*x^" + (i + 1));
            }
            Console.WriteLine();
            
            int[] shadows = new int[n];
            for (int i = 0; i < n; i++)
            {
                int nxt = rnd.Next(100);
                if (nxt!=0)
                    shadows[i] = nxt;
            }
            int[,] points = new int[n,2];
            Console.WriteLine("Shadows:");
            for(int i=0;i<n;i++)
            {
                points[i, 0] = shadows[i];
                points[i, 1] = f(k-1,shadows[i],coef,M);
                Console.WriteLine((i+1)+": f("+ points[i, 0]+")="+ points[i, 1]);
            }
            


            //retrieve secret
            int[,] keys = new int[k,2];
            double[,] keys_d = new double[k, 2];
            Console.WriteLine("Enter "+k+" points");
            for(int i=0;i<k;i++)
            {
                Console.WriteLine((i + 1));
                Console.WriteLine("x:");
                str = Console.ReadLine();
                Int32.TryParse(str, out keys[i, 0]);
                keys_d[i, 0] = keys[i, 0];
                Console.WriteLine("y:");
                str = Console.ReadLine();
                Int32.TryParse(str, out keys[i, 1]);
                keys_d[i, 1] = keys[i, 1];
            }

            double sum = 0;
            for(int i=0;i<k;i++)
            {
                sum += li(i, k, keys_d)*keys[i,1];
            }
            Console.WriteLine("Secret message retrieved: "+sum);
            Console.ReadKey();
        }
    }
}
