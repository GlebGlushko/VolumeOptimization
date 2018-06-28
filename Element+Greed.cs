using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{
    class Element
    {
        public int n { get; set;}
        public int c{ get; set;}   
        public List<int> p{ get; set;}
        public List<int> w{ get; set;}
        public List<bool> take{ get; set;}
        public Dictionary<KeyValuePair<int, int>, bool> E{ get; set;}
        
        public Element(int n, int c, List<int> p, List<int> w )
        {
            this.n = n;
            this.c = c;
            this.p = p;
            this.w = w;
            //this.E = E;
            this.take = new List<bool>();
            this.E = new Dictionary<KeyValuePair<int, int>, bool>();// else this.E = E;

        }

        public void AddForbidden(int x, int y)
        {
            
            this.E.Add(new KeyValuePair<int, int>(x, y), true);
            this.E.Add(new KeyValuePair<int, int>(y, x), true);
        }
        public override string ToString()
        {
            string res =  "weigts = ";
            foreach (int i in this.w)
                res += i.ToString() + ' ';
            res += "\npoints = ";
            foreach (int i in this.p)
                res += i.ToString() + ' ';
            return res;
            //+this.w.ToString() + "\n points = " + this.p.ToString();
        }
        public int Calculate()
        {
            for (int j = 0; j < this.n; ++j)
            {
                this.take.Add(false);
                bool flag = false;
                int sum = 0;
                for (int i = 0; i < j; ++i)
                {
                    if (this.take[i] && this.E.ContainsKey(new KeyValuePair<int, int>(i, j)))
                    {
                        flag = true;
                        break;
                    }
                    else
                        if (this.take[i]) sum += this.w[i];
                }
                if (sum + this.w[j] <= this.c && !flag) this.take[j] = true; 
            }
            int ans = 0;
            for (int i = 0; i < this.n; ++i)
                if (this.take[i]) ans += this.p[i];
            return ans;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            int n, c;
            List<int> p = new List<int>();
            List<int> w = new List<int>();
            n = Int32.Parse(Console.ReadLine());
            c = Int32.Parse(Console.ReadLine());
            string[] pp = Console.ReadLine().Split(' ');
            string[] ww = Console.ReadLine().Split(' ');
            for (int i = 0; i < pp.Length; ++i)
            {
                p.Add(Int32.Parse(pp[i]));
                w.Add(Int32.Parse(ww[i]));
            }
            Implict_enum check = new Implict_enum(n, c, p, w, 10);
            int m = Int32.Parse(Console.ReadLine());
            string[] xy;
            for (int i = 0; i < m; ++i)
            {
                xy = Console.ReadLine().Split(' ');
                check.AddForbidden(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
            }
            Console.WriteLine(check);
            List<bool> kk = new List<bool>();
            kk.Add(false);
            check.calcImplic(kk); 
            //check.calcImplic(new List<bool>(true));
        }
    }
}
