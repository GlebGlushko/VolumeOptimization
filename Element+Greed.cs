using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{
    class Element
    {
        public const double eps = 1e-5;
        public int n { get; set;}
        public int c{ get; set;}   
        public List<double> p{ get; set;}
        public List<double> w{ get; set;}
        public List<double> take{ get; set;}
        public Dictionary<KeyValuePair<int, int>, bool> E{ get; set;}
        
        public Element(int n, int c, List<double> p, List<double> w )
        {
            this.n = n;
            this.c = c;
            this.p = p;
            this.w = w;
            //this.E = E;
            this.take = new List<double>();
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
        public double Calculate()
        {
            for (int j = 0; j < this.n; ++j)
            {
                this.take.Add(0);
                bool flag = false;
                double sum = 0;
                for (int i = 0; i < j; ++i)
                {
                    if (Math.Abs(this.take[i]-1)<eps && this.E.ContainsKey(new KeyValuePair<int, int>(i+1, j+1)))
                    {
                        flag = true;
                        break;
                    }
                    else
                        if (Math.Abs(this.take[i]-1)<eps) sum += this.w[i];
                }
                if (sum + this.w[j] <= this.c && !flag) this.take[j] = 1; 
            }
            double ans = 0;
            for (int i = 0; i < this.n; ++i)
                if (Math.Abs(this.take[i] -1 )<eps) ans += this.p[i];
            return ans;
        }

    }

}

