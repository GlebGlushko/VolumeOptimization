#include<bits/stdc++.h>
#define ll long long
#define pb push_back
#define mkp make_pair

using namespace std;
int n,i,x,c,m,y, possible, ans, ans_i, sum;
bool flag;
vector <int> p,w;
set < pair <int, int> > E;
int dp[205][20005], take[1006] ;
int main(){
    cin>>n;
    cin>>c;
    p.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        p.pb(x);
    }
    w.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        w.pb(x);
    }
   // swap(w,p);
    cin>>m;

    for (int i=0;i<m;++i)
    {
        cin>>x>>y;
        E.insert(mkp(x,y));
        E.insert(mkp(y,x));
    }


}
