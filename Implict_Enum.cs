using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{

    class Node
    {
        //List<int> leaf;
        public List<bool> take { get; set; }
        public Node(List<bool> take)
        {
            //this.leaf = leaf;
            this.take = take;
           // this.F = new List<int>();
        }
    }
    class Implict_enum : Element
    {
        double minimum;
        List <Node> nodes;
        public Implict_enum(int n, int c, List<double> p, List<double> w, double minimum, Dictionary< KeyValuePair < int, int > , bool > Set): 
            base(n, c, p, w)
        {
            this.minimum = minimum;
            this.nodes = new List<Node>();
            this.E = Set;
        }

        public override string ToString()
        {
            return base.ToString() + "\nminimum = " + this.minimum.ToString() + "\nLength of list of nodes = " + this.nodes.Count.ToString();
        }
        public bool can()
        {
            this.calcImplic(new List<bool>());
            //Console.WriteLine("----------nodes------------");
            //this.writeNodes();
            //Console.WriteLine("----------nodes------------");

            foreach(Node i in this.nodes)
            {
                double sump = 0, sumw = 0;
                for (int j = 0; j < i.take.Count; ++j)
                    if (i.take[j])
                    {
                        sump += this.p[j];
                        sumw += this.w[j];
                    }

                if (sump >= this.minimum && sumw <= this.c)
                {
                    //Console.WriteLine("Sum P  = " + sump.ToString() + "Sum W = " + sumw.ToString());
                    //Console.WriteLine("----------nodes------------");
                    //string s = "";
                    //for (int j = 0; j < i.take.Count; ++j)
                        //if (i.take[j]) s+="1 "; else s+="0 "; 
                       // Console.WriteLine(s);
                       // Console.WriteLine("----------nodes------------");

                    return true;
                }
            }
            return false;
        }
        public void calcImplic (List<bool> t){

            int k = t.Count;
            //if (t.)
            string s = "";
            for (int i = 0; i < t.Count; ++i)
                if (t[i]) s += "1 "; else s += "0 ";
            //Console.WriteLine(s);
                //Console.WriteLine(" k = " + k.ToString());
                if (k >= this.n) return;
            List<bool> yes = new List<bool>();
            yes.AddRange(t);
            //int k = t.Count;
            List<bool> no = new List<bool>();
            no.AddRange(t);
            yes.Add(true);
            no.Add(false);
            
            double WsumYes = 0, WsumNo = 0;
            double PsumYes = 0, PsumNo = 0;
            for (int i=0;i< yes.Count; ++i)
            {
                if (yes[i]) WsumYes += this.w[i];
                if (no[i]) WsumNo += this.w[i];
                if (yes[i]) PsumYes += this.p[i];
                if (no[i]) PsumNo += this.p[i];

            }

            bool flag = false;
            Node next = new Node(no);
            for (int i = 0; i < this.nodes.Count; ++i)
            {
                Node last = new Node(t);
                if (isDominate(last, next))
                {
                    flag = true;
                    break;
                }
            }
            //Console.WriteLine(flag);
            if (!flag && PsumNo >= this.minimum) this.nodes.Add(new Node(no));
            if (!flag)  
                calcImplic(no);


            flag = false;
            for (int i = 0; i < yes.Count-1; ++i)
                if (yes[i])
                    if (this.E.ContainsKey(new KeyValuePair<int, int>(i+1, yes.Count)))
                {
                    flag = true;
                    break;
                }
            next = new Node(yes);
            for (int i = 0; i < this.nodes.Count; ++i)
            {
                Node last = new Node(t);
                if (isDominate(last, next))
                {
                    flag = true;
                    break;
                }
            }
            /////!!!if (!flag && PsumYes >= this.minimum) this.nodes.Add(new Node(yes));
            if (!flag && PsumYes >= this.minimum) this.nodes.Add(new Node(yes));
            if (!flag) calcImplic(yes);
            ////////////////////////
            

        }
        private bool isDominate(Node x, Node y)
        {
            //return false;
            double psum1 = 0, psum2 = 0, wsum1 = 0, wsum2 = 0;
            for (int i = 0; i < x.take.Count; ++i)
                if (x.take[i]) { psum1 += this.p[i]; wsum1 += this.w[i]; }
            for (int i = 0; i < y.take.Count; ++i)
                if (y.take[i]) { psum2 += this.p[i]; wsum2 += this.w[i]; }

            return (psum1 > psum2 && wsum1 < wsum2 && createF(x).IsSubsetOf(createF(y))); 
            
        }
        private HashSet<int> createF(Node x)
        {
            HashSet <int> res  = new HashSet<int>();
            for (int i=0;i<x.take.Count;++i)
            {
                for (int j=i+1; j<x.take.Count; ++j) 
                    if (this.E.ContainsKey(new KeyValuePair<int,int>(i,j))) res.Add(j);
            }
            return res;
        } 
        public void writeNodes(){
            for (int i=0;i<this.nodes.Count; ++ i)
            {
                string res = "";
                foreach (bool f in this.nodes[i].take)
                    if (f) res += "1 "; else res += "0 ";
                Console.WriteLine(res);
            }
        }
    }
}
