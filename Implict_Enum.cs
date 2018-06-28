using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greed
{

    class Node
    {
        List<int> leaf;
        public List<bool> take { get; set; }
        public Node(List<int> leaf, List<bool> take)
        {
            this.leaf = leaf;
            this.take = take;
           // this.F = new List<int>();
        }
    }
    class Implict_enum : Element
    {
        int minimum;
        List <Node> nodes;
        public Implict_enum(int n, int c, List<int> p, List<int> w, int minimum): 
            base(n, c, p, w)
        {
            this.minimum = minimum;
            this.nodes = new List<Node>();
        }

        public override string ToString()
        {
            return base.ToString() + "\nminimum = " + this.minimum.ToString() + "\nLength of list of nodes = " + this.nodes.Count.ToString();
        }

        public void calcImplic (List<bool> t){
            int k = t.Count;
            if (k >= this.n) return;
            List<bool> yes = new List<bool>();
            yes.AddRange(t);
            //int k = t.Count;
            List<bool> no = new List<bool>();
            no.AddRange(t);
            yes.Add(true);
            no.Add(false);
            
            int WsumYes = 0, WsumNo = 0;
            int PsumYes = 0, PsumNo = 0;
            for (int i=0;i<=k; ++i)
            {
                if (yes[i]) WsumYes += this.w[i];
                if (no[i]) WsumNo += this.w[i];
                if (yes[i]) PsumYes += this.p[i];
                if (no[i]) PsumNo += this.p[i];

            }
            bool flag = false;
            for (int i = 0; i < k; ++i)
                if (this.E.ContainsKey(new KeyValuePair<int, int>(k, i)))
                {
                    flag = true;
                    break;
                }
            Node next = new Node(null, yes);
            for (int i=0;i<this.nodes.Count;++i)
            {
                Node last = new Node(null,t);
                if (isDominate(last, next))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag) this.nodes.Add(new Node(null,yes));
            calcImplic(yes);
            ////////////////////////
            
            next = new Node(null, no);
            for (int i = 0; i < this.nodes.Count; ++i)
            {
                Node last = new Node(null, t);
                if (isDominate(last, next))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag) this.nodes.Add(new Node(null, no));


        }
        private bool isDominate(Node x, Node y)
        {
            int psum1 = 0, psum2 = 0, wsum1 = 0, wsum2 = 0;
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
    }
}
