using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{
    class main
    {
        public const double accuracy = 0.1;
        class Program
        {
            static void binary_search(double left, double right, int n, int c, List<double> p, List<double> w, Dictionary< KeyValuePair<int, int > , bool> Set)
            {
                //double middle = 0;
                while (right - left > accuracy)
                {
                    double middle = (right + left) / 2.0;
                    Implict_enum check = new Implict_enum(n, c, p, w, middle, Set);
                    if (check.can()) left = middle; else right = middle;
                    //Console.WriteLine("middle = " + middle.ToString());
                    //Console.WriteLine("left = " + left.ToString());
                    //Console.WriteLine("right = " + right.ToString());
                }
                Console.WriteLine("ans = " + Math.Round( right).ToString());
            }

            
            static void Main(string[] args)
            {
                int n, c;
                List<double> p = new List<double>();
                List<double> w = new List<double>();
                n = Int32.Parse(Console.ReadLine());
                c = Int32.Parse(Console.ReadLine());
                string[] pp = Console.ReadLine().Split(' ');
                string[] ww = Console.ReadLine().Split(' ');
                for (int i = 0; i < pp.Length; ++i)
                {
                    p.Add(Int32.Parse(pp[i]));
                    w.Add(Int32.Parse(ww[i]));
                }
                int m = Int32.Parse(Console.ReadLine());
                string[] xy;
                List<double> lp = (new List<double>());
                lp.AddRange(p);
                List<double> lw = (new List<double>());
                lw.AddRange(w);
                Local_Search check_local = new Local_Search(n, c, p , w);
                Lagrange check_lagrange = new Lagrange(n, c, lp, lw);
                Dictionary<KeyValuePair<int, int>, bool> Set = new Dictionary<KeyValuePair<int, int>, bool>();

                for (int i = 0; i < m; ++i)
                {
                    xy = Console.ReadLine().Split(' ');

                    check_local.AddForbidden(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
                    check_lagrange.AddForbidden(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
                    Set.Add(new KeyValuePair<int, int>(Int32.Parse(xy[0]), Int32.Parse(xy[1])), true);
                    //Set.Add(new KeyValuePair<int, int>(Int32.Parse(xy[1]), Int32.Parse(xy[0])), true);

                    //Set.Add(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
                }
                double left = check_local.Search();
                double right = check_lagrange.LagrangeF();
                Console.WriteLine("Local_Serach = " + left.ToString());
                Console.WriteLine("Lagrange = " + right.ToString());
                //Implict_enum check_implicit = new Implict_enum(n,xy,p,w,)
                //binary_search(0, 1000, n, c, p, w, Set);
                //Console.WriteLine("max points: ");
                binary_search(left, right, n, c, p, w, Set);


                //Console.WriteLine("Local_Serach = " + check_local.Search().ToString());
                /*
                Implict_enum check = new Implict_enum(n, c, p, w, 0);
                int m = Int32.Parse(Console.ReadLine());
                string[] xy;
                for (int i = 0; i < m; ++i)
                {
                    xy = Console.ReadLine().Split(' ');
                    check.AddForbidden(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
                }
                Console.WriteLine(check);
                List<bool> kk = new List<bool>();
                kk.Add(true );
                check.calcImplic(kk);
                check.writeNodes();
                //check.calcImplic(new List<bool>(true));*/
            }
        }
    }
}


/*
 
10
200
32 52 64 35 96 18 66 15 13 4
3 6 9 12 38 9 77 40 96 95
11
1 2
1 3 
1 9
3 6
3 7
4 5
4 9
5 6
7 9
8 9
8 10
 
  
  
  
 
10
200
32 52 64 35 96 18 66 15 13 4
3 6 9 12 38 9 77 40 96 95
9
1 2
1 3 
1 9
3 6
3 7
4 9
5 6
7 9
8 9

 
 
*/