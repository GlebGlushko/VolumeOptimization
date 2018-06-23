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
    vector <int> take;
    set < pair < int, int > > E;
    void restore(){
        for (int i=1;i<take.size();++i)
            if (take[i]) cout<<i<< ' ';
        cout<<"\n";
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
    take.pb(0);
        for (int j=1; j<=n; ++j)
        {
            take.pb(0);
            bool flag = false;
            int sum =0;
            for (int i=1;i<j;++i)
            if (take[i] && E.find(mkp(i,j))!=E.end()){
                flag = true;
                break;
            } else sum+=val[i].w*take[i];
            take[j] =  !(sum>c || flag);
            //cout<<take[j]<<' '<<sum<<"\n";
        }
    //cout<<"----------\n";
    int ans = 0;
    for (int i=1;i<=n;++i)
        ans+=take[i]*val[i].p;

    if (flag) restore();
    return ans;

        //return dp[n][c];
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


