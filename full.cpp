
#include<bits/stdc++.h>
#define ll long long
#define pb push_back
#define mkp make_pair

using namespace std;
int n,i,x,c,m,y, possible, ans, ans_i;
vector <int> p,w;
set < pair <int, int> > E;
int dp[205][20005] ;
int main(){
    cin>>n;
    cin>>c;
    //p.pb(0);
    for (int i=0;i<n;++i){
        cin>>x;
        p.pb(x);
    }
    //w.pb(0);
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
    //cout<<(1<<n)<<endl;;
    for (int num=0;num<(1<<n);++num){
        int k=num;
        bool f=false;
        for (int i=n-1;i>=0;--i)
          for (int j=n-1;j>=0;--j)
            if ((k>>i)%2&&(k>>j)%2)
                if (E.find(mkp(i+1,j+1))!=E.end()) f= true;
        if (f) continue;
        int possible = 0;
        int sum = 0;
        for (int i=n-1;i>=0;--i)
            possible += (k>>i)%2 * p[i],
            sum+=(k>>i)%2 * w[i];
        if (possible>ans&&sum<=c) {
            ans=possible;
            ans_i = num;
        }
        //cout<<num<<' ' << possible<<"\n";
//        cout<<"\n";
    }

    cout<<ans<<"\n";
    for (int i=n-1; i>=0; --i)
        if ((ans_i>>i)%2) cout<<i+1<<' ';



}
/*
4 6
6 2 5 1
2 4 1 2
3
1 2
1 3
1 4

6
1
*/
