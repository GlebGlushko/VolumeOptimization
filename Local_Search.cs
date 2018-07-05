using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{
    class Local_Search:Element
    {
        private double[] better; 
        public Local_Search(int n, int c, List<double> p, List<double> w) : base(n, c, p, w) {
            this.better = new double[n];
            //for (int i = 0; i < n; ++i)
            //    this.better[i] = 0;
        }

        public double Search()
        {
            double start = this.Calculate();
            for (int i = 0; i < this.n; ++i)
                if (this.take[i] > eps)
                    for (int j = i+1; j < this.n; ++j)
                    {
                        if (this.take[j] < eps)
                        {
                            bool flag = false;
                            for (int u=0;u<this.n;++u)
                                if (u==i) continue; else 
                                if (this.E.ContainsKey(new KeyValuePair<int,int>(j+1, u+1))) { flag = true; break;}
                            if (!flag)
                            {
                                take[i] = 0;
                                take[j] = 1;
                                double sp =0, sw = 0;
                                for (int u=0;u<this.n;++u)
                                { 
                                   sp += this.p[u] * this.take[u];
                                   sw += this.w[u] * this.take[u];
                                }
                                if (sw < this.c && start < sp)
                                {
                                    start = sp;
                                    for (int u = 0; u < this.n; ++u)
                                        this.better[u] = this.take[u];
                                    this.take[j] = 0;
                                    this.take[i] = 1;
                                }

                            }
                        }
                    }
            return start;   
        }
    }
}
