#include<bits/stdc++.h>
#define w first
#define p second
#define pb push_back
#define mkp make_pair
using namespace std;
int n, c, x, m, y;
vector < int > pp, ww;

class element{
private:
    vector < pair < int, int > > val;

    int dp[105][10005];
    set < pair < int, int > > E;

    bool check(int k, int s, int cant){
    if (dp[k][s]==0) return true;
    if (dp[k-1][s] == dp[k][s]) return check(k-1,s, cant); else
        if (E.find(mkp(k,cant))!=E.end()) return false;
            else return check(k-1, s-val[k].w, cant);
}
void restore(int k, int s){

    if (!dp[k][s]) return;

    if (dp[k-1][s]==dp[k][s]) restore(k-1,s); else
    {
        cout<<k<<' ';
        restore(k-1, s - val[k].w);
    }
}
public:
    void create(vector < int > points, vector < int > weights ){
        for (int i=0;i<weights.size();++i)
            val.push_back(mkp(weights[i], points[i]));
    }
    void add (int x, int y){
        E.insert(mkp(x,y));
        E.insert(mkp(y,x));
    }
    void print(){
        cout<<"weights: ";
        for (int i=1;i<val.size();++i)
            cout<<setw(4)<<right<<val[i].w<<' ';
        cout<<"\n";
        cout<<"points:  ";
        for (int i=1;i<val.size();++i)
            cout<<setw(4)<<right<<val[i].p<<' ';
        cout<<"\n";

    }

    int calculate( int c, bool flag = false){

        for (int i=0;i<=c;++i)
            dp[0][i] = 0;
        int n = val.size();
        for (int i=0;i<=n;++i)
            dp[i][0] = 0;

        for (int k = 1; k <= n; ++k)
        for (int s = 0; s <= c; ++s){
            if (s >= val[k].w && check(k-1,s,k))
                dp[k][s] = max( dp[k-1][s], dp[k-1][s-val[k].w]+val[k].p);
            else
                dp[k][s] = dp[k-1][s];
        }
        if (flag) restore(n,c);
        return dp[n][c];
    }

};


int main(){

    cin>>n;
    cin>>c;

    pp.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        pp.pb(x);
    }
    ww.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        ww.pb(x);
    }
    element *ch = new element;
    ch->create(pp,ww);


    cin>>m;

    for (int i=0;i<m;++i)
    {
        cin>>x>>y;
        ch->add(x,y);
    }

    ch->print();
    cout<<ch->calculate(c)<<"\n";
    ch->calculate(c,1);
}

