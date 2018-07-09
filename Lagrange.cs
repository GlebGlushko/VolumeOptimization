using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{
    class Lagrange : Element
    {
        private const double e = 0.2;
        private Dictionary<KeyValuePair<int, int>, double> lyabda;
        private List<double> pCof;
        private int[] pos;
        public Lagrange(int n, int c, List<double> p, List<double> w) :
            base(n, c, p, w)
        {
            this.pos = new int[n];
            this.lyabda = new Dictionary<KeyValuePair<int, int>, double>();
            this.pCof = new List<double>();
            for (int i = 0; i < this.n; ++i)
            {
                this.pos[i] = i;
                this.pCof.Add(1);
                for (int j = 0; j < this.n; ++j)
                    this.lyabda.Add(new KeyValuePair<int, int>(i + 1, j + 1), 0);
                this.take.Add(0);
         
            }
        }

        public void calculate()
        {
           
                for (int i = 0; i < this.n; ++i)
                    this.take[i] = 0;

            for (int j = 0; j < this.n; ++j)
                {
                    //this.take.Add(0);
                    bool flag = false;
                    double sum = 0;
                    for (int i = 0; i < j; ++i)
                    {
                        if (Math.Abs(this.take[this.pos[i]]) > eps && this.E.ContainsKey(new KeyValuePair<int, int>(Math.Min(this.pos[i] + 1, this.pos[j] + 1), Math.Max(this.pos[j] + 1, this.pos[i] + 1))))
                        {
                            flag = true;
                            break;
                        }
                        else
                            sum += this.w[this.pos[i]] * this.take[this.pos[i]];
                    }
                    if (flag)
                    {
                        this.take[this.pos[j]] = 0;
                        continue;
                    }
                    //if (flag) Console.WriteLine("rtkgjdfgdfhkjdhbfdkdshdfsdgjhsdfjgksdfjkgbsdhfkgbdfgsdfgsdfg");
                    //if (sum + this.w[this.pos[j]] <= this.c ) this.take[this.pos[j]] = 1;
                    //Console.WriteLine("sum = " + sum.ToString());
                    //Console.WriteLine("------------------------------------------------------------kek oru sum = " + (sum+this.w[this.pos[j]]).ToString());
                    if (sum + this.w[this.pos[j]] <= this.c) this.take[this.pos[j]] = 1; 
                                                        else this.take[this.pos[j]] = (c - sum) / this.w[this.pos[j]];
                    //Console.WriteLine("new_take = " + this.take[this.pos[j]].ToString());
                }


            //double ans = 0;
            //for (int i = 0; i < this.n; ++i)
            //    ans += this.p[this.pos[i]] * this.take[this.pos[i]];
            //Console.WriteLine("sum = ");
            //Console.WriteLine(ans);

            //return ans;
        }
        private void sort()
        {
            //Console.WriteLine("sorting.....");
            for (int i=0;i<this.n; ++i)
                 for (int j=0;j<i;++j)
                     if (this.p[this.pos[i]] / this.w[this.pos[i]] > this.p[this.pos[j]] / this.w[this.pos[j]])
                     {
                         //double change = this.p[this.pos[i]];
                         //this.p[this.pos[i]] = this.p[this.pos[j]];
                         //this.p[this.pos[j]] = change;
                         //change = this.w[this.pos[i]];
                         //this.w[this.pos[i]] = this.w[this.pos[j]];
                         //this.w[this.pos[j]] = change;
                         int change_pos = this.pos[i];
                         this.pos[i] = this.pos[j];
                         this.pos[j] = change_pos;
                     }
           
        }
        public double LagrangeF(double L = 0)
        {

            ///////pre solution
            double ans = 0, pre_sum = 0;
            for (int i = 0; i < this.n; ++i)
            {
                ans+= this.p[i] * Math.Min(1.0,(this.c - pre_sum)/this.w[i]);
                //pre_sum += Math.Max(0, this.c - pre_sum);
                if (this.c - pre_sum >= this.w[i]) pre_sum += this.w[i];
                else break;
            }
                //ans += this.p[i];
            return ans;
            ///////pre solition

            this.sort();
            Console.WriteLine("-----------resort-----------");

            for (int i = 0; i < this.n; ++i)
                Console.WriteLine(this.pos[i]);
            Console.WriteLine("-----------resort-----------");

                this.calculate();
            //Console.WriteLine("-----------take-----------");

            //for (int i = 0; i < this.n; ++i)
            //    Console.WriteLine(this.take[this.pos[i]]);
                
            //Console.WriteLine("-----------take-----------");
                for (int i = 0; i < this.n; ++i)
                {
                    for (int j = 0; j < this.n; ++j)
                    {
                        if (this.take[this.pos[i]] < eps && this.take[this.pos[j]] < eps) continue;
                        KeyValuePair<int, int> key = new KeyValuePair<int, int>(Math.Min(this.pos[i] + 1, this.pos[j] + 1), Math.Max(this.pos[j] + 1, this.pos[i] + 1));
                        if (this.E.ContainsKey(key))
                        {
                            this.lyabda[key] = 1 - this.take[this.pos[i]] - this.take[this.pos[j]];
                            //Console.WriteLine("E lymbda refreshing...");
                            //Console.WriteLine(1 - this.take[this.pos[i]] - this.take[this.pos[j]]);
                        }
                        
                    }
                }
            double lymbdaSum  = 0;
            //Console.WriteLine("-----------E lyambda-----------");


            for (int i = 0; i < this.n; ++i) this.pCof[i] = 1;


            for (int i = 0; i < this.n; ++i)
                    for (int j = 0; j < i; ++j)
                    {
                        KeyValuePair<int, int> key = new KeyValuePair<int, int>(Math.Min(this.pos[i] + 1, this.pos[j] + 1), Math.Max(this.pos[j] + 1, this.pos[i] + 1));
                        if (this.E.ContainsKey(key))
                        {
                            //Console.WriteLine("key = " + key.ToString() + "  lyambda[key] = " + this.lyabda[key] );
                            this.pCof[this.pos[i]] -= this.lyabda[key];
                            //this.p[this.pos[i]] -= this.lyabda[key];
                            //this.p[this.pos[j]] -= this.lyabda[key];

                            lymbdaSum += this.lyabda[key] * (1 - this.take[this.pos[i]] - this.take[this.pos[j]]);
                            //Console.WriteLine(this.lyabda[key]);
                        }

                    }
            //Console.WriteLine("-----------E lyambda-----------");

            //Console.WriteLine("-----------pCof-----------");

            //for (int i = 0; i < this.n; ++i)
            //    Console.WriteLine(this.pCof[this.pos[i]]);

            //Console.WriteLine("-----------pCof-----------");

            double sum = 0;
            //Console.WriteLine("-----------pCof-----------");

            for (int i = 0; i < this.n; ++i)
            {
                //Console.WriteLine(this.pCof[this.pos[i]]);
                //this.p[this.pos[i]] *= this.pCof[this.pos[i]];
                this.pCof[this.pos[i]] = 1;
                if (this.take[this.pos[i]] > eps) this.p[this.pos[i]] *= this.take[this.pos[i]];
                //Console.WriteLine(this.take[i]);this.p
                sum += this.take[this.pos[i]]  * this.p[this.pos[i]];
            }
            //Console.WriteLine("-----------El-----------");

            Console.WriteLine("sum = " + sum.ToString());
            //Console.WriteLine("-----------p-----------");

            //for (int i = 0; i < this.n; ++i)
            //    Console.WriteLine(this.p[this.pos[i]]);

            //Console.WriteLine("-----------p-----------");
            //Console.WriteLine("-----------take-----------");

            //for (int i = 0; i < this.n; ++i)
            //    Console.WriteLine(this.take[this.pos[i]]);

            //Console.WriteLine("-----------take-----------");
            if (Math.Abs(sum + lymbdaSum - L) >= e) return LagrangeF(sum + lymbdaSum);
            else
            {
                
                return sum;


            }
            //else 
            //Console.WriteLine("SUMMA = " + sum.ToString());  
        }
    }
}
